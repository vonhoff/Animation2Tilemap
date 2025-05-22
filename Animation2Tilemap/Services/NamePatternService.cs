﻿using System.Diagnostics;
using System.Text.RegularExpressions;
using Animation2Tilemap.Services.Contracts;
using Serilog;

namespace Animation2Tilemap.Services;

public partial class NamePatternService : INamePatternService
{
    private readonly ILogger _logger;
    private readonly Regex _namePattern = NamePattern();
    private readonly Regex _namePatternAlt = NamePatternAlternative();

    public NamePatternService(ILogger logger)
    {
        _logger = logger;
    }

    public string? GetMostNotablePattern(List<string> names)
    {
        var stopwatch = Stopwatch.StartNew();
        var patternCount = CountPatterns(names, _namePattern);
        _logger.Information("Found {PatternCount} name pattern(s). Took: {Elapsed}ms", patternCount.Count,
            stopwatch.ElapsedMilliseconds);

        var patternCountAlt = CountPatterns(names, _namePatternAlt);
        _logger.Information("Found {PatternCount} alternative name pattern(s). Took: {Elapsed}ms",
            patternCountAlt.Count, stopwatch.ElapsedMilliseconds);

        var maxPattern = FindMaxPattern(patternCount);
        var maxPatternAlt = FindMaxPattern(patternCountAlt);

        if (maxPattern != null && IsPresentInAll(names, maxPattern))
        {
            stopwatch.Stop();
            _logger.Information("A notable name pattern is {MaxPattern}. Took: {Elapsed}ms", maxPattern,
                stopwatch.ElapsedMilliseconds);
            return maxPattern;
        }

        if (maxPatternAlt != null && IsPresentInAll(names, maxPatternAlt))
        {
            stopwatch.Stop();
            _logger.Information("A notable alternative name pattern is {MaxPatternAlt}. Took: {Elapsed}ms",
                maxPatternAlt, stopwatch.ElapsedMilliseconds);
            return maxPatternAlt;
        }

        stopwatch.Stop();
        _logger.Warning("Could not find a repeating name pattern. Took: {Elapsed}ms", stopwatch.ElapsedMilliseconds);
        return null;
    }

    private static Dictionary<string, int> CountPatterns(IEnumerable<string> names, Regex regex)
    {
        var patternCount = new Dictionary<string, int>();
        foreach (var name in names)
        foreach (Match match in regex.Matches(name))
        {
            var pattern = match.Value;
            patternCount.TryGetValue(pattern, out var count);
            patternCount[pattern] = count + 1;
        }

        return patternCount;
    }

    private static string? FindMaxPattern(Dictionary<string, int> patternCount)
    {
        string? maxPattern = null;
        var maxCount = 0;
        var longestLength = 0;

        foreach (var (pattern, count) in patternCount)
            if (count > maxCount || (count == maxCount && pattern.Length > longestLength))
            {
                maxPattern = pattern;
                maxCount = count;
                longestLength = pattern.Length;
            }

        return maxPattern;
    }

    [GeneratedRegex(@"(?<!\p{L})\p{L}[\p{L}\p{N}\p{Pd}\p{Pc}]*\p{L}(?!\p{L})")]
    private static partial Regex NamePattern();

    [GeneratedRegex(@"(?<!\p{L})\p{L}[\p{L}\d]*\p{L}(?!\p{L})")]
    private static partial Regex NamePatternAlternative();

    private bool IsPresentInAll(IEnumerable<string> strings, string pattern)
    {
        var regex = new Regex(pattern, RegexOptions.Compiled);

        if (strings.Any(str => !regex.IsMatch(str)))
        {
            _logger.Information("Candidate name pattern {Pattern} does not occur in all filenames.", pattern);
            return false;
        }

        _logger.Information("Candidate name pattern {Pattern} does occur in all filenames.", pattern);
        return true;
    }
}
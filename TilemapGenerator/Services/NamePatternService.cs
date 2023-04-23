using System.Diagnostics;
using System.Text.RegularExpressions;
using Serilog;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public partial class NamePatternService : INamePatternService
    {
        private readonly ILogger _logger;
        private readonly Regex _regex = NamePattern();

        public NamePatternService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets the most occurring pattern of letter characters in a list of strings.
        /// </summary>
        /// <param name="strings">The list of strings to search for patterns in.</param>
        /// <returns>The most occurring pattern of letter characters in the list of strings.</returns>
        /// <remarks>
        /// This method searches through each string in the input <paramref name="strings"/> list,
        /// and extracts all possible patterns of letter characters from each string. <br/>
        /// The method then determines the most occurring pattern of letter characters
        /// among all the strings and returns it.
        /// </remarks>
        public string? GetMostOccurringPattern(List<string> strings)
        {
            var stopwatch = Stopwatch.StartNew();
            var patternCounts = new Dictionary<string, int>();

            foreach (var str in strings)
            {
                foreach (Match match in _regex.Matches(str))
                {
                    var patternString = match.Value;
                    if (patternCounts.ContainsKey(patternString))
                    {
                        patternCounts[patternString]++;
                    }
                    else
                    {
                        patternCounts[patternString] = 1;
                    }
                }
            }

            string? mostCommonPattern = null;
            var mostCommonCount = 0;

            foreach (var (pattern, count) in patternCounts)
            {
                if (count > mostCommonCount)
                {
                    mostCommonPattern = pattern;
                    mostCommonCount = count;
                }
            }

            if (mostCommonPattern == null)
            {
                _logger.Warning("Could not find a repeating name pattern in the provided name list. Took: {elapsed}ms",
                    stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.Information("The most occurring name pattern is {mostCommonPattern}. Took: {elapsed}ms",
                    mostCommonPattern, stopwatch.ElapsedMilliseconds);
            }

            return mostCommonPattern;
        }

        [GeneratedRegex("(?<!\\p{L})\\p{L}[\\p{L}\\p{N}_]*\\p{L}(?!\\p{L})")]
        private static partial Regex NamePattern();
    }
}
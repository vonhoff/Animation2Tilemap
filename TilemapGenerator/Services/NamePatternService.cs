using System.Diagnostics;
using System.Text;
using Serilog;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class NamePatternService : INamePatternService
    {
        private readonly ILogger _logger;

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
                for (var i = 0; i < str.Length; i++)
                {
                    // Find the start of a pattern
                    if (!char.IsLetter(str[i]))
                    {
                        continue;
                    }

                    var pattern = new StringBuilder();
                    pattern.Append(str[i]);

                    // Continue building the pattern until a non-letter character is encountered
                    for (var j = i + 1; j < str.Length; j++)
                    {
                        if (char.IsLetter(str[j]))
                        {
                            pattern.Append(str[j]);
                        }
                        else if (str[j] == '_' && j > i + 1 && j < str.Length - 1 && char.IsLetter(str[j - 1]) && char.IsLetter(str[j + 1]))
                        {
                            pattern.Append(str[j]);
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Add the pattern to the dictionary or increment its count if it already exists
                    var patternString = pattern.ToString();
                    if (patternString.Length > 1)
                    {
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

        /// <summary>
        /// Gets the most occurring letter in a list of strings.
        /// </summary>
        /// <param name="strings">The list of strings to search for letters in.</param>
        /// <returns>The most occurring letter in the list of strings.</returns>
        /// <remarks>
        /// This method searches through each string in the input <paramref name="strings"/> list, <br/>
        /// and counts the occurrences of each letter. The method then determines the most occurring letter <br/>
        /// among all the strings and returns it. If no letters are found in the input strings, <br/>
        /// the method will return an empty string.
        /// </remarks>
        public string? GetMostOccurringLetter(List<string> strings)
        {
            var stopwatch = Stopwatch.StartNew();
            var letterCounts = strings
                .Where(str => !string.IsNullOrEmpty(str))
                .SelectMany(str => str.Where(char.IsLetter))
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            if (letterCounts.Count == 0)
            {
                _logger.Warning("Could not find a repeating letter in the provided name list. Took: {elapsed}ms", stopwatch.ElapsedMilliseconds);
                return null;
            }

            var mostCommonLetter = letterCounts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            _logger.Information("The most occurring letter is {Letter}. Took {elapsed}ms", mostCommonLetter, stopwatch.ElapsedMilliseconds);
            return mostCommonLetter.ToString();
        }
    }
}
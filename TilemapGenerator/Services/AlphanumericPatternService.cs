using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class AlphanumericPatternService : IAlphanumericPatternService
    {
        /// <summary>
        /// Gets the most occurring pattern of alphanumeric characters in a list of strings.
        /// </summary>
        /// <param name="strings">The list of strings to search for patterns in.</param>
        /// <returns>The most occurring pattern of alphanumeric characters in the list of strings.</returns>
        /// <remarks>
        /// This method searches through each string in the input <paramref name="strings"/> list,
        /// and extracts all possible patterns of alphanumeric characters from each string. <br/>
        /// The method then determines the most occurring pattern of alphanumeric characters
        /// among all the strings and returns it.
        /// </remarks>
        public string? GetMostOccurringPattern(List<string> strings)
        {
            var patternCounts = new Dictionary<string, int>();

            foreach (var str in strings)
            {
                var inWord = false;
                var wordStartIndex = -1;

                for (var i = 0; i < str.Length; i++)
                {
                    var c = str[i];

                    if (char.IsLetterOrDigit(c))
                    {
                        if (!inWord)
                        {
                            inWord = true;
                            wordStartIndex = i;
                        }
                    }
                    else
                    {
                        if (inWord)
                        {
                            inWord = false;
                            var wordLength = i - wordStartIndex;
                            ExtractPatterns(str, wordStartIndex, wordLength, patternCounts);
                        }
                    }
                }

                if (inWord)
                {
                    var wordLength = str.Length - wordStartIndex;
                    ExtractPatterns(str, wordStartIndex, wordLength, patternCounts);
                }
            }

            string? mostCommonPattern = null;
            var mostCommonCount = 0;
            var longestPatternLength = 0;

            foreach (var (pattern, count) in patternCounts)
            {
                if (count > mostCommonCount || count == mostCommonCount && pattern.Length > longestPatternLength && pattern.Length > 1)
                {
                    mostCommonPattern = pattern;
                    mostCommonCount = count;
                    longestPatternLength = pattern.Length;
                }
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
            var letterCounts = strings
                .Where(str => !string.IsNullOrEmpty(str))
                .SelectMany(str => str.Where(char.IsLetter))
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());

            if (letterCounts.Count == 0)
            {
                return null;
            }

            var mostCommonLetter = letterCounts.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return mostCommonLetter.ToString();
        }

        /// <summary>
        /// Extracts all possible patterns of alphanumeric characters from a substring and updates a dictionary with the count of each pattern found.
        /// </summary>
        /// <param name="str">The input string to extract patterns from.</param>
        /// <param name="startIndex">The starting index of the substring within the input string.</param>
        /// <param name="length">The length of the substring.</param>
        /// <param name="patternCounts">The dictionary to update with the count of each pattern found.</param>
        /// <remarks>
        /// This method extracts all possible patterns of alphanumeric characters from the substring starting at
        /// <paramref name="startIndex"/> and with a length of <paramref name="length"/>, and updates the dictionary
        /// <paramref name="patternCounts"/> with the count of each pattern found. Patterns with a length of 1 or less
        /// are excluded from consideration because they are not considered to be patterns of alphanumeric characters.
        /// </remarks>
        private static void ExtractPatterns(ReadOnlySpan<char> str, int startIndex, int length, IDictionary<string, int> patternCounts)
        {
            if (length <= 1)
            {
                return;
            }

            for (var i = 2; i <= length; i++)
            {
                for (var j = 0; j <= length - i; j++)
                {
                    var pattern = str.Slice(startIndex + j, i).ToString();

                    if (patternCounts.ContainsKey(pattern))
                    {
                        patternCounts[pattern]++;
                    }
                    else
                    {
                        patternCounts[pattern] = 1;
                    }
                }
            }
        }
    }
}
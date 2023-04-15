namespace TilemapGenerator.Utilities
{
    public static class ImageNameUtility
    {
        /// <summary>
        /// Returns the most commonly occurring pattern of alphanumeric characters in a list of strings.
        /// </summary>
        /// <param name="strings">The list of strings to search for patterns.</param>
        /// <returns>The most commonly occurring pattern of alphanumeric characters, or an empty string if no pattern is found.</returns>
        /// <remarks>
        /// This method searches each string in the list for patterns of alphanumeric characters
        /// and returns the most commonly occurring pattern.
        /// Patterns with a length of 1 are excluded from consideration, as are patterns with the
        /// same count as the most common pattern but a shorter length.
        /// </remarks>
        public static string GetMostOccurringPattern(List<string> strings)
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

            var mostCommonPattern = string.Empty;
            var mostCommonCount = 0;
            var longestPatternLength = 0;

            foreach (var (pattern, count) in patternCounts)
            {
                if (count > mostCommonCount || (count == mostCommonCount && pattern.Length > longestPatternLength && pattern.Length > 1))
                {
                    mostCommonPattern = pattern;
                    mostCommonCount = count;
                    longestPatternLength = pattern.Length;
                }
            }

            return mostCommonPattern;
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
        /// <paramref name="startIndex"/> and with a length of <paramref name="length"/>, <br/>
        /// and updates the dictionary <paramref name="patternCounts"/> with the count of each pattern found.
        /// Patterns with a length of 1 are excluded from consideration.
        /// </remarks>
        private static void ExtractPatterns(string str, int startIndex, int length, IDictionary<string, int> patternCounts)
        {
            for (var i = 2; i <= length; i++)
            {
                for (var j = 0; j <= length - i; j++)
                {
                    var pattern = str.Substring(startIndex + j, i);

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
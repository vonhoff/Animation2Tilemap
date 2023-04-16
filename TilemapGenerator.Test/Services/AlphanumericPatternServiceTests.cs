using TilemapGenerator.Contracts;
using TilemapGenerator.Services;

namespace TilemapGenerator.Test.Services
{
    public class AlphanumericPatternServiceTests
    {
        private readonly IAlphanumericPatternService _patternService = new AlphanumericPatternService();

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsEnglishLettersAndUnderscores()
        {
            var strings = new List<string> {
                "4_dragon_01",
                "6_dragon_31",
                "8_dragon_03",
                "88_dragon_31",
                "999_dragon_441",
                "3_dragon_021",
                "46_dragon_01",
                "43_dragon_08",
                "994_dragon_021"
            };

            var result = _patternService.GetMostOccurringPattern(strings);

            Assert.Equal("dragon", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsEnglishLettersAndSpaces()
        {
            var strings = new List<string> {
                "girjplayer",
                "rgeplayerde",
                "efdcplayer",
                "ddfjplayer",
                "ifiejfplayer",
                "playerijfie",
                "playerijfe",
                "playeriji",
                "983player"
            };

            var result = _patternService.GetMostOccurringPattern(strings);

            Assert.Equal("player", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsRussianLetters()
        {
            var strings = new List<string> {
                "___синица___0",
                "xxxсиница2",
                "sssсиница",
                "dfdfdсиница",
                "3синица2",
                "синица2",
                "синица3"
            };

            var result = _patternService.GetMostOccurringPattern(strings);

            Assert.Equal("синица", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsChineseCharacters()
        {
            var strings = new List<string> {
                "青蛙",
                "青蛙2",
                "熊猫",
                "猴子",
                "猴子2",
                "熊猫2",
                "熊猫3"
            };

            var result = _patternService.GetMostOccurringPattern(strings);

            Assert.Equal("熊猫", result);
        }
    }
}
using TilemapGenerator.Utilities;

namespace TilemapGenerator.Test.Utilities
{
    public class ImageNameUtilityTests
    {
        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsEnglishLettersAndUnderscores()
        {
            // Arrange
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

            // Act
            var result = ImageNameUtility.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("dragon", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsEnglishLettersAndSpaces()
        {
            // Arrange
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

            // Act
            var result = ImageNameUtility.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("player", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsRussianLetters()
        {
            // Arrange
            var strings = new List<string> {
                "белка",
                "белка2",
                "синица",
                "журавль",
                "журавль2",
                "синица2",
                "синица3"
            };

            // Act
            var result = ImageNameUtility.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("синица", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsChineseCharacters()
        {
            // Arrange
            var strings = new List<string> {
                "青蛙",
                "青蛙2",
                "熊猫",
                "猴子",
                "猴子2",
                "熊猫2",
                "熊猫3"
            };

            // Act
            var result = ImageNameUtility.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("熊猫", result);
        }
    }
}
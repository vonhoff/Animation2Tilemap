using Serilog;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;
using Xunit.Abstractions;

namespace TilemapGenerator.Test.Services
{
    public class NamePatternServiceTests
    {
        private readonly INamePatternService _patternService;

        public NamePatternServiceTests(ITestOutputHelper testOutputHelper)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
                .CreateLogger();

            _patternService = new NamePatternService(logger);
        }

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
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("dragon", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsMultiplePatterns()
        {
            // Arrange
            var strings = new List<string> {
                "telephone",
                "telephone_add",
                "telephone_delete",
                "telephone_edit",
                "telephone_error",
                "telephone_go",
                "telephone_key",
                "telephone_link"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("telephone", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenPatternContainNumbers()
        {
            // Arrange
            var strings = new List<string> {
                "player_v2_walk1",
                "player_v2_walk2",
                "player_v2_walk3",
                "player_v2_walk4",
                "player_v2_walk5",
                "player_v2_walk6",
                "player_v2_walk7",
                "player_v2_walk8"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("player_v2_walk", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsUnderscores()
        {
            // Arrange
            var strings = new List<string> {
                "player_shoot_11",
                "player_shoot_12",
                "player_shoot_13",
                "player_shoot_14",
                "player_shoot_15",
                "player_shoot_16",
                "player_shoot_17",
                "player_shoot_18"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("player_shoot", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnNull_WhenInputContainsSingleLetters()
        {
            // Arrange
            var strings = new List<string> {
                "a",
                "a",
                "a",
                "b",
                "b",
                "0",
                "b",
                "a",
                "a"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsIndexedNames()
        {
            // Arrange
            var strings = new List<string> {
                "barrel1",
                "barrel2",
                "barrel3",
                "barrel4",
                "barrel5",
                "barrel6",
                "barrel7",
                "barrel8",
                "barrel9"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("barrel", result);
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
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("player", result);
        }

        [Fact]
        public void GetMostOccurringPattern_ShouldReturnCommonPattern_WhenInputContainsRussianLetters()
        {
            // Arrange
            var strings = new List<string> {
                "___синица___0",
                "xxxсиница2",
                "sssсиница",
                "dfdfdсиница",
                "3синица2",
                "синица2",
                "синица3"
            };

            // Act
            var result = _patternService.GetMostOccurringPattern(strings);

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
            var result = _patternService.GetMostOccurringPattern(strings);

            // Assert
            Assert.Equal("熊猫", result);
        }

        [Fact]
        public void GetMostOccurringLetter_ShouldReturnCommonLetter_WhenInputContainsSingleCharacters()
        {
            // Arrange
            var strings = new List<string> {
                "青",
                "青",
                "熊",
                "熊",
                "猴",
                "熊",
                "熊"
            };

            // Act
            var result = _patternService.GetMostOccurringLetter(strings);

            // Assert
            Assert.Equal("熊", result);
        }

        [Fact]
        public void GetMostOccurringLetter_ShouldReturnCommonLetter_WhenInputContainsSentences()
        {
            // Arrange
            var strings = new List<string> {
                "The fox jumps over the box.",
                "Mix the ingredients well before baking the cake.",
                "Max is an expert in complex mathematical equations.",
                "The taxi driver drove us to the apex of the mountain.",
                "I accidentally spilled my coffee on the fax machine.",
                "The next exit on the highway is for the town of Apex.",
                "The annex of the building was recently renovated."
            };

            // Act
            var result = _patternService.GetMostOccurringLetter(strings);

            // Assert
            Assert.Equal("e", result);
        }

        [Fact]
        public void GetMostOccurringLetter_ShouldReturnNull_WhenInputIsEmpty()
        {
            var strings = new List<string> {
                string.Empty,
                string.Empty
            };

            var result = _patternService.GetMostOccurringLetter(strings);

            Assert.Null(result);
        }
    }
}
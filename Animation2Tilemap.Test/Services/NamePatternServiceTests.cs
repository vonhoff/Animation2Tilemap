using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Test.TestHelpers;
using Serilog;
using Xunit.Abstractions;

namespace Animation2Tilemap.Test.Services;

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
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenInputContainsLettersAndUnderscores()
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
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("dragon", result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenInputContainsDifferentNames()
    {
        // Arrange
        var strings = new List<string> {
            "application_add",
            "application_delete",
            "application_something",
            "application_create",
            "application_update",
            "application_what",
            "application_reload",
            "application_string",
            "application_list"
        };

        // Act
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("application", result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenPatternContainNumbers()
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
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("player_v2_walk", result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenInputContainsUnderscores()
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
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("player_shoot", result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenInputContainsDashes()
    {
        // Arrange
        var strings = new List<string> {
            "player-shoot-11",
            "player-shoot-12",
            "player-shoot-13",
            "player-shoot-14",
            "player-shoot-15",
            "player-shoot-16",
            "player-shoot-17",
            "player-shoot-18"
        };

        // Act
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("player-shoot", result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnNull_WhenInputContainsSingleLetters()
    {
        // Arrange
        var strings = new List<string> {
            "a",
            "a",
            "a"
        };

        // Act
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetMostNotablePattern_ShouldReturnCommonPattern_WhenInputContainsIndexedNames()
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
        var result = _patternService.GetMostNotablePattern(strings);

        // Assert
        Assert.Equal("barrel", result);
    }
}
using System.CommandLine;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class FpsOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-10)]
    public void FpsOption_InvalidDuration_ReturnsError(int? duration)
    {
        // Arrange
        var frameDurationOption = new FpsOption();
        var command = new Command("test");

        // Act
        frameDurationOption.Register(command);
        var result = command.Parse($"--fps {duration}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(1000)]
    public void FpsOption_ValidDuration_ReturnsValue(int duration)
    {
        // Arrange
        var frameDurationOption = new FpsOption();
        var command = new Command("test");

        // Act
        frameDurationOption.Register(command);
        var result = command.Parse($"--fps {duration}");

        // Assert
        Assert.Equal(duration, result.GetValueForOption(frameDurationOption.Option));
    }
}
using System.CommandLine;
using Animation2Tilemap.Console.CommandLineOptions;

namespace Animation2Tilemap.Console.Test.CommandLineOptions;

public class FrameDurationOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-10)]
    public void FrameDurationOption_InvalidDuration_ReturnsError(int? duration)
    {
        // Arrange
        var frameDurationOption = new FrameDurationOption();
        var command = new Command("test");

        // Act
        frameDurationOption.Register(command);
        var result = command.Parse($"--duration {duration}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(50)]
    [InlineData(1000)]
    public void FrameDurationOption_ValidDuration_ReturnsValue(int duration)
    {
        // Arrange
        var frameDurationOption = new FrameDurationOption();
        var command = new Command("test");

        // Act
        frameDurationOption.Register(command);
        var result = command.Parse($"--duration {duration}");

        // Assert
        Assert.Equal(duration, result.GetValueForOption(frameDurationOption.Option));
    }
}
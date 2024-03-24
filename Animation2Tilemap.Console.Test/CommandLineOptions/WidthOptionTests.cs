using Animation2Tilemap.Console.CommandLineOptions;
using System.CommandLine;

namespace Animation2Tilemap.Console.Test.CommandLineOptions;

public class WidthOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    public void WidthOption_InvalidWidth_ReturnsError(int? width)
    {
        // Arrange
        var widthOption = new WidthOption();
        var command = new Command("test");

        // Act
        widthOption.Register(command);
        var result = command.Parse($"--width {width}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(16)]
    public void WidthOption_ValidWidth_ReturnsValue(int width)
    {
        // Arrange
        var command = new Command("test");
        var widthOption = new WidthOption();

        // Act
        widthOption.Register(command);
        var result = command.Parse($"--width {width}");

        // Assert
        Assert.Equal(width, result.GetValueForOption(widthOption.Option));
    }
}
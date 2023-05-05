using System.CommandLine;
using Xunit;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class HeightOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    public void HeightOption_InvalidHeight_ReturnsError(int? height)
    {
        // Arrange
        var heightOption = new HeightOption();
        var command = new Command("test");

        // Act
        heightOption.Register(command);
        var result = command.Parse($"--height {height}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(16)]
    public void HeightOption_ValidHeight_ReturnsValue(int height)
    {
        // Arrange
        var heightOption = new HeightOption();
        var command = new Command("test");

        // Act
        heightOption.Register(command);
        var result = command.Parse($"--height {height}");

        // Assert
        Assert.Equal(height, result.GetValueForOption(heightOption.Option));
    }
}
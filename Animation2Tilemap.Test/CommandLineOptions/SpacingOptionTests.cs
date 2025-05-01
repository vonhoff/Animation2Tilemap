using System.CommandLine;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class SpacingOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void SpacingOption_InvalidSpacing_ReturnsError(int? spacing)
    {
        // Arrange
        var spacingOption = new SpacingOption();
        var command = new Command("test");

        // Act
        spacingOption.Register(command);
        var result = command.Parse($"--spacing {spacing}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(16)]
    public void SpacingOption_ValidSpacing_ReturnsValue(int spacing)
    {
        // Arrange
        var spacingOption = new SpacingOption();
        var command = new Command("test");

        // Act
        spacingOption.Register(command);
        var result = command.Parse($"--spacing {spacing}");

        // Assert
        Assert.Equal(spacing, result.GetValueForOption(spacingOption.Option));
    }
}
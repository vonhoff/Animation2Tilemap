using System.CommandLine;
using Animation2Tilemap.Console.CommandLineOptions;

namespace Animation2Tilemap.Console.Test.CommandLineOptions;

public class TransparentColorOptionTests
{
    [Theory]
    [InlineData("#ffffff")]
    [InlineData("ffffff")]
    [InlineData("#12345678")]
    [InlineData("12345678")]
    public void Register_ValidColors_DoNotSetErrorMessage(string color)
    {
        // Arrange
        var command = new Command("test");
        var transparentColorOption = new TransparentColorOption();

        // Act
        transparentColorOption.Register(command);
        var result = command.Parse($"--transparent {color}");

        // Assert
        Assert.Empty(result.Errors);
        Assert.Equal(color, result.GetValueForOption(transparentColorOption.Option));
    }

    [Theory]
    [InlineData("")]
    [InlineData("#xyz123")]
    [InlineData("not-a-color")]
    public void Register_InvalidColors_SetErrorMessage(string color)
    {
        // Arrange
        var command = new Command("test");
        var option = new TransparentColorOption();

        // Act
        option.Register(command);
        var result = command.Parse($"--transparent {color}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }
}
using Animation2Tilemap.CommandLineOptions;
using System.CommandLine;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class InputOptionTests
{
    [Fact]
    public void InputOption_InvalidPath_ReturnsError()
    {
        // Arrange
        var inputOption = new InputOption();
        var command = new Command("test");

        // Act
        inputOption.Register(command);
        var result = command.Parse("--input \" \"");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void InputOption_ValidPath_ReturnsValue()
    {
        // Arrange
        var inputOption = new InputOption();
        var command = new Command("test");
        const string path = "test_input.txt";

        // Act
        inputOption.Register(command);
        var result = command.Parse($"--input \"{path}\"");

        // Assert
        Assert.Equal(path, result.GetValueForOption(inputOption.Option));
    }
}
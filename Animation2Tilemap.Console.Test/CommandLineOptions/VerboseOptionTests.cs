using System.CommandLine;
using Animation2Tilemap.Console.CommandLineOptions;

namespace Animation2Tilemap.Console.Test.CommandLineOptions;

public class VerboseOptionTests
{
    [Fact]
    public void Register_NoVerboseFlag_DefaultValueFalse()
    {
        // Arrange
        var command = new Command("test");
        var verboseOption = new VerboseOption();

        // Act
        verboseOption.Register(command);
        var result = command.Parse("");

        // Assert
        Assert.False(result.GetValueForOption(verboseOption.Option));
    }

    [Fact]
    public void Register_VerboseFlagTrue_SetValueTrue()
    {
        // Arrange
        var command = new Command("test");
        var verboseOption = new VerboseOption();

        // Act
        verboseOption.Register(command);
        var result = command.Parse("--verbose");

        // Assert
        Assert.True(result.GetValueForOption(verboseOption.Option));
    }
}
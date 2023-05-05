using System.CommandLine;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class VerboseOptionTests
{
    [Fact]
    public void Register_NoVerboseFlag_DefaultValueFalse()
    {
        // Arrange
        var command = new Command("test");
        var option = new VerboseOption();

        // Act
        option.Register(command);
        var result = command.Parse("");

        // Assert
        Assert.False(result.GetValueForOption(option.Option));
    }

    [Fact]
    public void Register_VerboseFlagTrue_SetValueTrue()
    {
        // Arrange
        var command = new Command("test");
        var option = new VerboseOption();

        // Act
        option.Register(command);
        var result = command.Parse("--verbose");

        // Assert
        Assert.True(result.GetValueForOption(option.Option));
    }
}
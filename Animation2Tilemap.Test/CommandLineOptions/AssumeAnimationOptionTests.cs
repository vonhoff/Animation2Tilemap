using Animation2Tilemap.CommandLineOptions;
using System.CommandLine;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class AssumeAnimationOptionTests
{
    [Fact]
    public void Register_NoAssumeAnimationFlag_DefaultValueFalse()
    {
        // Arrange
        var command = new Command("test");
        var assumeAnimationOption = new AssumeAnimationOption();

        // Act
        assumeAnimationOption.Register(command);
        var result = command.Parse("");

        // Assert
        Assert.False(result.GetValueForOption(assumeAnimationOption.Option));
    }

    [Fact]
    public void Register_AssumeAnimationFlagTrue_SetValueTrue()
    {
        // Arrange
        var command = new Command("test");
        var assumeAnimationOption = new AssumeAnimationOption();

        // Act
        assumeAnimationOption.Register(command);
        var result = command.Parse("--assume-animation");

        // Assert
        Assert.True(result.GetValueForOption(assumeAnimationOption.Option));
    }
} 
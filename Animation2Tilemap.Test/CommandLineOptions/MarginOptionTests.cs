﻿using System.CommandLine;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class MarginOptionTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void MarginOption_InvalidMargin_ReturnsError(int? margin)
    {
        // Arrange
        var marginOption = new MarginOption();
        var command = new Command("test");

        // Act
        marginOption.Register(command);
        var result = command.Parse($"--margin {margin}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(8)]
    [InlineData(16)]
    public void MarginOption_ValidMargin_ReturnsValue(int margin)
    {
        // Arrange
        var marginOption = new MarginOption();
        var command = new Command("test");

        // Act
        marginOption.Register(command);
        var result = command.Parse($"--margin {margin}");

        // Assert
        Assert.Equal(margin, result.GetValueForOption(marginOption.Option));
    }
}
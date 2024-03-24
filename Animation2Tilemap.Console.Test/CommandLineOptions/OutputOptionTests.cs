using Animation2Tilemap.Console.CommandLineOptions;
using System.CommandLine;

namespace Animation2Tilemap.Console.Test.CommandLineOptions;

public class OutputOptionTests
{
    [Fact]
    public void Register_InvalidOutputFolder_ReturnsError()
    {
        // Arrange
        var outputOption = new OutputOption();
        var command = new Command("test");
        var invalidPath = Path.GetInvalidPathChars()[0].ToString();

        // Act
        outputOption.Register(command);
        var result = command.Parse($"--output {invalidPath}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }

    [Fact]
    public void Register_ValidOutputFolder_CreatesFolder()
    {
        // Arrange
        var command = new Command("test");
        var outputOption = new OutputOption();
        var tempFolder = Path.GetTempPath();
        var expectedFolderPath = Path.Combine(tempFolder, "test-folder");

        // Act
        outputOption.Register(command);
        var result = command.Parse($"--output {expectedFolderPath}");

        // Assert
        Assert.Equal(expectedFolderPath, result.GetValueForOption(outputOption.Option));
        Assert.True(Directory.Exists(expectedFolderPath));
    }
}
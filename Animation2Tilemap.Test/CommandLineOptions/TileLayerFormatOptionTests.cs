using System.CommandLine;
using Animation2Tilemap.CommandLineOptions;

namespace Animation2Tilemap.Test.CommandLineOptions;

public class TileLayerFormatOptionTests
{
    [Theory]
    [InlineData("zlib")]
    [InlineData("gzip")]
    [InlineData("csv")]
    [InlineData("base64")]
    public void Register_ValidFormats_DoNotSetErrorMessage(string format)
    {
        // Arrange
        var command = new Command("test");
        var option = new TileLayerFormatOption();

        // Act
        option.Register(command);
        var result = command.Parse($"--format {format}");

        // Assert
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("unknown")]
    [InlineData("bzip2")]
    public void Register_InvalidFormats_SetErrorMessage(string format)
    {
        // Arrange
        var command = new Command("test");
        var option = new TileLayerFormatOption();

        // Act
        option.Register(command);
        var result = command.Parse($"--format {format}");

        // Assert
        Assert.NotEmpty(result.Errors);
    }
}
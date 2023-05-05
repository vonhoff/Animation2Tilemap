using Animation2Tilemap.Enums;
using Animation2Tilemap.Services;
using Animation2Tilemap.Test.TestHelpers;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit.Abstractions;

namespace Animation2Tilemap.Test.Services;

public class ImageAlignmentServiceTests
{
    private readonly ImageAlignmentService _imageAlignmentService;
    private readonly ApplicationOptions _options;

    public ImageAlignmentServiceTests(ITestOutputHelper testOutputHelper)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
            .CreateLogger();

        _options = new ApplicationOptions(0, string.Empty, string.Empty,
            new Size(16, 16), Rgba32.ParseHex("FF00FF"), TileLayerFormat.Base64GZip, false);

        _imageAlignmentService = new ImageAlignmentService(logger, _options);
    }

    [Fact]
    public void TryAlignImage_ShouldReturnTrueAndAlignImages_GivenValidInput()
    {
        // Arrange
        const string fileName = "test.png";
        var frames = new List<Image<Rgba32>>
        {
            new(10, 10),
            new(15, 15),
            new(20, 20)
        };

        // Act
        var result = _imageAlignmentService.TryAlignImage(fileName, frames);

        // Assert
        Assert.True(result);
        Assert.Equal(16, frames[0].Width);
        Assert.Equal(16, frames[0].Height);
        Assert.Equal(16, frames[1].Width);
        Assert.Equal(16, frames[1].Height);
        Assert.Equal(32, frames[2].Width);
        Assert.Equal(32, frames[2].Height);
    }

    [Fact]
    public void TryAlignImage_ShouldApplyTransparentColorToAlignedImages_GivenValidInput()
    {
        // Arrange
        const string fileName = "test.png";
        var frames = new List<Image<Rgba32>>
        {
            new(10, 10),
            new(15, 15),
            new(20, 20)
        };

        // Act
        var result = _imageAlignmentService.TryAlignImage(fileName, frames);

        // Assert
        Assert.True(result);

        // Check if the transparent color is applied to the top left pixel of each frame
        Assert.Equal(_options.TransparentColor, frames[0][0, 0]);
        Assert.Equal(_options.TransparentColor, frames[1][0, 0]);
        Assert.Equal(_options.TransparentColor, frames[2][0, 0]);
    }
}
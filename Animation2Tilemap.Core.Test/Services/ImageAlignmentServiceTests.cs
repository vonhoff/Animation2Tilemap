using Animation2Tilemap.Core.Services;
using Animation2Tilemap.Core.Test.TestHelpers;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit.Abstractions;

namespace Animation2Tilemap.Core.Test.Services;

public class ImageAlignmentServiceTests
{
    private readonly ImageAlignmentService _imageAlignmentService;

    public ImageAlignmentServiceTests(ITestOutputHelper testOutputHelper)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
            .CreateLogger();

        var options = new ApplicationOptions
        {
            TileSize = new Size(16, 16)
        };

        _imageAlignmentService = new ImageAlignmentService(logger, options);
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
}
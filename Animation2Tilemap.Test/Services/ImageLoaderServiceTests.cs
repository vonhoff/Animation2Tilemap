using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Test.TestHelpers;
using Animation2Tilemap.Workflows;
using Moq;
using Serilog;
using Serilog.Core;
using Xunit.Abstractions;

namespace Animation2Tilemap.Test.Services;

public class ImageLoaderServiceTests
{
    private readonly Mock<IConfirmationDialogService> _confirmationDialogServiceMock = new();

    private readonly Logger _logger;

    private readonly Mock<INamePatternService> _namePatternServiceMock = new();

    public ImageLoaderServiceTests(ITestOutputHelper testOutputHelper)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
            .CreateLogger();
    }

    [Fact]
    public void TryLoadImages_ShouldReturnFalse_WhenInputPathIsInvalid()
    {
        // Arrange
        const string path = "invalid path";
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.False(result);
        Assert.Empty(images);
    }

    [Fact]
    public void TryLoadImages_ShouldReturnFalse_WhenInputPathDoesNotLeadToAnyValidImages()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath("Invalid");
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.False(result);
        Assert.Empty(images);
    }

    [Fact]
    public void TryLoadImages_ShouldLoadSingleImage_WhenInputPathPointsToFile()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath(Path.Combine("Images", "кошка.png"));
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Single(images);
        Assert.Single(images.First().Value);
        Assert.Equal(100, images.First().Value[0].FrameTime); // Default frame time for non-GIF
    }

    [Fact]
    public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath(Path.Combine("Images", "dollarspindownd.gif"));
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Single(images);
        Assert.Equal(18, images.First().Value.Count);
        // Verify that frame times are present (they should be non-zero for a GIF)
        Assert.All(images.First().Value, frame => Assert.True(frame.FrameTime > 0));
    }

    [Fact]
    public void TryLoadImages_ShouldProcessFramesIndividually_WhenInputPathPointsToDirectory()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath("Frames");
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Equal(8, images.Count);

        foreach (var image in images)
        {
            Assert.Single(image.Value);
            Assert.Equal(100, image.Value[0].FrameTime); // Default frame time for non-GIF
        }
    }

    [Fact]
    public void TryLoadImages_ShouldProcessFramesAsAnimation_WhenInputPathPointsToDirectory()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath("Frames");
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new MainWorkflowOptions { Input = path });

        _confirmationDialogServiceMock.Setup(c => c.Confirm(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
        _namePatternServiceMock.Setup(n => n.GetMostNotablePattern(It.IsAny<List<string>>())).Returns("anim");

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Equal(8, images["anim"].Count);
        Assert.Single(images);

        foreach (var frame in images["anim"])
        {
            Assert.Equal(100, frame.FrameTime); // Default frame time for non-GIF
        }
    }
}
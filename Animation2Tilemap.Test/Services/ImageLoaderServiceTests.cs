using Animation2Tilemap.Enums;
using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Test.TestHelpers;
using Moq;
using Serilog;
using Serilog.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit.Abstractions;

namespace Animation2Tilemap.Test.Services;

public class ImageLoaderServiceTests
{
    private readonly Mock<IConfirmationDialogService> _confirmationDialogServiceMock;
    private readonly Mock<INamePatternService> _namePatternServiceMock;
    private readonly Logger _logger;

    public ImageLoaderServiceTests(ITestOutputHelper testOutputHelper)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
            .CreateLogger();
        _confirmationDialogServiceMock = new Mock<IConfirmationDialogService>();
        _namePatternServiceMock = new Mock<INamePatternService>();
    }

    [Fact]
    public void TryLoadImages_ShouldReturnFalse_WhenInputPathIsInvalid()
    {
        // Arrange
        const string path = "invalid path";
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

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
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

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
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Single(images);
        Assert.Single(images.First().Value);
    }

    [Fact]
    public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath(Path.Combine("Images", "dollarspindownd.gif"));
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Single(images);
        Assert.Equal(18, images.First().Value.Count);
    }

    [Fact]
    public void TryLoadImages_ShouldProcessFramesIndividually_WhenInputPathPointsToDirectory()
    {
        // Arrange
        var path = TestResourcesHelper.GetPath("Frames");
        var imageLoader = new ImageLoaderService(_logger,
            _namePatternServiceMock.Object,
            _confirmationDialogServiceMock.Object,
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Equal(8, images.Count);

        foreach (var image in images)
        {
            Assert.Single(image.Value);
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
            new ApplicationOptions(0, path, "", Size.Empty, 0, 0, new Rgba32(), TileLayerFormat.Base64ZLib, false));

        _confirmationDialogServiceMock.Setup(c => c.Confirm(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
        _namePatternServiceMock.Setup(n => n.GetMostNotablePattern(It.IsAny<List<string>>())).Returns("anim");

        // Act
        var result = imageLoader.TryLoadImages(out var images);

        // Assert
        Assert.True(result);
        Assert.Equal(8, images["anim"].Count);
        Assert.Equal(1, images.Count);

        foreach (var image in images["anim"])
        {
            Assert.Single(image.Frames);
        }
    }
}
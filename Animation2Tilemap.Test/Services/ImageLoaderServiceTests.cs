//using Animation2Tilemap.Services;
//using Animation2Tilemap.Services.Contracts;
//using Moq;
//using Serilog;
//using Serilog.Core;
//using Xunit.Abstractions;

//namespace Animation2Tilemap.Test.Services;

//public class ImageLoaderServiceTests
//{
//    private readonly Mock<IConfirmationDialogService> _confirmationDialogServiceMock;
//    private readonly Mock<INamePatternService> _namePatternServiceMock;
//    private readonly Logger _logger;

//    public ImageLoaderServiceTests(ITestOutputHelper testOutputHelper)
//    {
//        _logger = new LoggerConfiguration()
//            .MinimumLevel.Verbose()
//            .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
//            .CreateLogger();
//        _confirmationDialogServiceMock = new Mock<IConfirmationDialogService>();
//        _namePatternServiceMock = new Mock<INamePatternService>();
//    }

//    [Fact]
//    public void TryLoadImages_ShouldReturnFalse_WhenInputPathIsInvalid()
//    {
//        // Arrange
//        const string path = "invalid path";
//        var imageLoader = new ImageLoaderService(_logger, 
//            _namePatternServiceMock.Object, 
//            _confirmationDialogServiceMock.Object, 
//            new ApplicationOptions());

//        // Act
//        var result = imageLoader.TryLoadImages(out var images);

//        // Assert
//        Assert.False(result);
//        Assert.Empty(images);
//    }

//    [Fact]
//    public void TryLoadImages_ShouldReturnFalse_WhenInputPathDoesNotLeadToAnyValidImages()
//    {
//        // Arrange
//        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvalidFolder");

//        // Act
//        var result = _imageLoader.TryLoadImages(out var images);

//        // Assert
//        Assert.False(result);
//        Assert.Empty(images);
//    }

//    [Fact]
//    public void TryLoadImages_ShouldLoadSingleImage_WhenInputPathPointsToFile()
//    {
//        // Arrange
//        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "кошка.png");

//        // Act
//        var result = _imageLoader.TryLoadImages(out var images);

//        // Assert
//        Assert.True(result);
//        Assert.Single(images);
//        Assert.Single(images.First().Value);
//    }

//    [Fact]
//    public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
//    {
//        // Arrange
//        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "dollarspindownd.gif");

//        // Act
//        var result = _imageLoader.TryLoadImages(out var images);

//        // Assert
//        Assert.True(result);
//        Assert.Single(images);
//        Assert.Equal(18, images.First().Value.Count);
//    }

//    [Fact]
//    public void TryLoadImages_ShouldLoadImagesLikeAnimationFrames_WhenInputPathPointsToDirectory()
//    {
//        // Arrange
//        var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AnimationFrames");

//        // Act
//        var result = _imageLoader.TryLoadImages(out var images);

//        // Assert
//        Assert.True(result);
//        Assert.Equal(8, images.Count);

//        foreach (var image in images)
//        {
//            Assert.Single(image.Value);
//        }
//    }
//}
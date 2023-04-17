using Serilog;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;
using Xunit.Abstractions;

namespace TilemapGenerator.Test.Services
{
    public class ImageLoaderServiceTests
    {
        private readonly IImageLoaderService _imageLoader;

        public ImageLoaderServiceTests(ITestOutputHelper testOutputHelper)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
                .CreateLogger();

            _imageLoader = new ImageLoaderService(logger);
        }

        [Fact]
        public void TryLoadImages_ShouldReturnFalse_WhenInputPathIsInvalid()
        {
            // Arrange
            const string path = "invalid path";

            // Act
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            // Assert
            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldReturnFalse_WhenInputPathDoesNotLeadToAnyValidImages()
        {
            // Arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvalidFolder");

            // Act
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            // Assert
            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadSingleImage_WhenInputPathPointsToFile()
        {
            // Arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "кошка.png");

            // Act
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            // Assert
            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Single(images.First().Value);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
        {
            // Arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "dollarspindownd.gif");

            // Act
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            // Assert
            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Equal(18, images.First().Value.Count);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadImagesLikeAnimationFrames_WhenInputPathPointsToDirectory()
        {
            // Arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AnimationFrames");

            // Act
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            // Assert
            Assert.True(result);
            Assert.Equal(8, images.Count);
            Assert.True(suitableForAnimation);

            foreach (var image in images)
            {
                Assert.Single(image.Value);
            }
        }
    }
}
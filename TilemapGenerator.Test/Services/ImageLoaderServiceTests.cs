using Serilog;
using TilemapGenerator.Contracts;
using TilemapGenerator.Services;
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
            const string path = "invalid path";
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldReturnFalse_WhenInputPathDoesNotLeadToAnyValidImages()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvalidFolder");
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadSingleImage_WhenInputPathPointsToFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "кошка.png");
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Single(images.First().Value);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "dollarspindownd.gif");
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Equal(18, images.First().Value.Count);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadImagesLikeAnimationFrames_WhenInputPathPointsToDirectory()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AnimationFrames");
            var result = _imageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);

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
using TilemapGenerator.Utilities;

namespace TilemapGenerator.Test.Utilities
{
    public class ImageLoaderUtilityTests
    {
        [Fact]
        public void TryLoadImages_ShouldReturnFalse_WhenInputPathIsInvalid()
        {
            const string path = "invalid path";
            var result = ImageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);
            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldReturnFalse_WhenInputPathDoesNotLeadToAnyValidImages()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "InvalidFolder");
            var result = ImageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);
            Assert.False(result);
            Assert.Empty(images);
            Assert.False(suitableForAnimation);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadSingleImage_WhenInputPathPointsToFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "кошка.png");
            var result = ImageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);
            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Single(images.First().Value);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadGifAnimation_WhenInputPathPointsToFile()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Unicates", "dollarspindownd.gif");
            var result = ImageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);
            Assert.True(result);
            Assert.Single(images);
            Assert.False(suitableForAnimation);
            Assert.Equal(18, images.First().Value.Count);
        }

        [Fact]
        public void TryLoadImages_ShouldLoadImagesLikeAnimationFrames_WhenInputPathPointsToDirectory()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "AnimationFrames");
            var result = ImageLoader.TryLoadImages(path, out var images, out var suitableForAnimation);
            Assert.True(result);
            Assert.Equal(8, images.Count);
            Assert.True(suitableForAnimation);

            foreach (var image in images)
            {
                Assert.Equal(1, image.Value.Count);
            }
        }
    }
}
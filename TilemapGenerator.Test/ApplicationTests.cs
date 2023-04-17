using Moq;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Services.Contracts;
using Xunit.Abstractions;

namespace TilemapGenerator.Test
{
    public class ApplicationTests
    {
        private readonly Mock<IAlphanumericPatternService> _alphanumericPatternServiceMock;
        private readonly Mock<IImageLoaderService> _imageLoaderServiceMock;
        private readonly Mock<IImageAlignmentService> _imageAlignmentServiceMock;
        private readonly Application _application;

        public ApplicationTests(ITestOutputHelper testOutputHelper)
        {
            _alphanumericPatternServiceMock = new Mock<IAlphanumericPatternService>();
            _imageLoaderServiceMock = new Mock<IImageLoaderService>();
            _imageAlignmentServiceMock = new Mock<IImageAlignmentService>();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
                .CreateLogger();

            _application = new Application(
                _alphanumericPatternServiceMock.Object,
                _imageLoaderServiceMock.Object,
                _imageAlignmentServiceMock.Object,
                logger);
        }

        [Fact]
        public void LoadAndAlignImages_WhenImagesNotLoaded_ReturnsFalse()
        {
            // Arrange
            var options = new CommandLineOptions(false, 0, "input.png", "output.png", 32, 32, "#FF00FF", true);
            var images = new Dictionary<string, List<Image<Rgba32>>>();
            _imageLoaderServiceMock
                .Setup(x => x.TryLoadImages(options.Input, out images, out It.Ref<bool>.IsAny))
                .Returns(false);

            // Act
            var result = _application.LoadAndAlignImages(options, out images);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LoadAndAlignImages_ReturnsTrue_WhenImagesAreLoadedAndAligned()
        {
            // Arrange
            var options = new CommandLineOptions(false, 0, "input", "output", 32, 32, "#FFFFFF", true);
            var images = new Dictionary<string, List<Image<Rgba32>>>();
            var expectedImages = new Dictionary<string, List<Image<Rgba32>>>();
            var image1 = new Image<Rgba32>(32, 32);
            var image2 = new Image<Rgba32>(32, 32);
            images.Add("image1.png", new List<Image<Rgba32>> { image1, image2 });
            _imageLoaderServiceMock
                .Setup(x => x.TryLoadImages(options.Input, out images, out It.Ref<bool>.IsAny))
                .Returns(true);

            var expectedImage1 = new Image<Rgba32>(32, 32);
            var expectedImage2 = new Image<Rgba32>(32, 32);
            expectedImages.Add("image1.png", new List<Image<Rgba32>> { expectedImage1, expectedImage2 });
            _imageAlignmentServiceMock
                .Setup(x => x.AlignFrame(image1, options.TileSize, options.TransparentColor))
                .Returns(expectedImage1);
            _imageAlignmentServiceMock
                .Setup(x => x.AlignFrame(image2, options.TileSize, options.TransparentColor))
                .Returns(expectedImage2);

            // Act
            var result = _application.LoadAndAlignImages(options, out var actualImages);

            // Assert
            Assert.True(result);
            Assert.Equal(expectedImages.Keys, actualImages.Keys);
            foreach (var key in expectedImages.Keys)
            {
                Assert.Equal(expectedImages[key].Count, actualImages[key].Count);
                for (var i = 0; i < expectedImages[key].Count; i++)
                {
                    Assert.Equal(expectedImages[key][i], actualImages[key][i]);
                }
            }
            _imageAlignmentServiceMock.Verify(
                x => x.AlignFrame(It.IsAny<Image<Rgba32>>(), options.TileSize, options.TransparentColor),
                Times.Exactly(2));
            _imageLoaderServiceMock.VerifyAll();
            _imageAlignmentServiceMock.VerifyAll();
        }

        [Fact]
        public void TransformImagesToAnimation_ShouldTransformImagesToAnimation()
        {
            // Arrange
            var inputImages = new Dictionary<string, List<Image<Rgba32>>>
            {
                {
                    "image1.png", new List<Image<Rgba32>>
                    {
                        new(45, 22),
                        new(12, 6)
                    }
                },
                {
                    "image2.png", new List<Image<Rgba32>>
                    {
                        new(33, 66),
                        new(7, 9)
                    }
                }
            };

            var expectedOutputImages = new Dictionary<string, List<Image<Rgba32>>>
            {
                {
                    "pattern", new List<Image<Rgba32>>
                    {
                        new(45, 22),
                        new(33, 66)
                    }
                }
            };

            _alphanumericPatternServiceMock
                .Setup(m => m.GetMostOccurringPattern(It.IsAny<List<string>>()))
                .Returns("pattern");

            // Act
            _application.TransformImagesToAnimation(ref inputImages);

            // Assert
            Assert.Equal(1, inputImages.Count);
            Assert.Equal(2, inputImages["pattern"].Count);
            Assert.Equal(expectedOutputImages["pattern"][0].Size, inputImages["pattern"][0].Size);
            Assert.Equal(expectedOutputImages["pattern"][1].Size, inputImages["pattern"][1].Size);
        }
    }
}
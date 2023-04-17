using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Test.Services
{
    public class ImageAlignmentServiceTests
    {
        private readonly IImageAlignmentService _imageAlignmentService = new ImageAlignmentService();

        [Fact]
        public void AlignFrame_ShouldReturnAlignedImage()
        {
            // Arrange
            var frame = new Image<Rgba32>(10, 10);
            var tileSize = new Size(4, 4);
            var backgroundColor = Rgba32.ParseHex("FFC0CB");

            // Act
            var alignedFrame = _imageAlignmentService.AlignFrame(frame, tileSize, backgroundColor);

            // Assert
            Assert.Equal(12, alignedFrame.Width);
            Assert.Equal(12, alignedFrame.Height);
            Assert.Equal(backgroundColor, alignedFrame[0, 0]);
            Assert.Equal(backgroundColor, alignedFrame[11, 11]);
        }

        [Fact]
        public void AlignFrame_ShouldReturnInputSizeIfAlreadyAligned()
        {
            // Arrange
            var frame = new Image<Rgba32>(16, 8);
            var tileSize = new Size(4, 4);
            var backgroundColor = Rgba32.ParseHex("FFF");

            // Act
            var alignedFrame = _imageAlignmentService.AlignFrame(frame, tileSize, backgroundColor);

            // Assert
            Assert.Equal(frame.Width, alignedFrame.Width);
            Assert.Equal(frame.Height, alignedFrame.Height);
        }
    }
}
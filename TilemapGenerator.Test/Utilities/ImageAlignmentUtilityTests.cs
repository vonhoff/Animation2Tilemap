using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Utilities;

namespace TilemapGenerator.Test.Utilities
{
    public class ImageAlignmentUtilityTests
    {
        [Fact]
        public void AlignFrame_ShouldReturnAlignedImage()
        {
            var frame = new Image<Rgba32>(10, 10);
            var tileSize = new Size(4, 4);
            var backgroundColor = Rgba32.ParseHex("FFC0CB");
            var alignedFrame = ImageAlignmentUtility.AlignFrame(frame, tileSize, backgroundColor);

            Assert.Equal(12, alignedFrame.Width);
            Assert.Equal(12, alignedFrame.Height);
            Assert.Equal(backgroundColor, alignedFrame[0, 0]);
            Assert.Equal(backgroundColor, alignedFrame[11, 11]);
        }

        [Fact]
        public void AlignFrame_ReturnsInputFrameIfAlreadyAligned()
        {
            var frame = new Image<Rgba32>(16, 8);
            var tileSize = new Size(4, 4);
            var backgroundColor = Rgba32.ParseHex("FFF");
            var alignedFrame = ImageAlignmentUtility.AlignFrame(frame, tileSize, backgroundColor);
            
            Assert.Equal(frame.Width, alignedFrame.Width);
            Assert.Equal(frame.Height, alignedFrame.Height);
        }
    }
}
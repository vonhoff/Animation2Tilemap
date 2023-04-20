using Moq;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Factories;
using TilemapGenerator.Services.Contracts;
using Xunit.Abstractions;

namespace TilemapGenerator.Test.Factories
{
    public class TilesetTileFactoryTests
    {
        private readonly Mock<ITileHashService> _hashServiceMock;
        private readonly TilesetTileFactory _tilesetTileFactory;

        public TilesetTileFactoryTests(ITestOutputHelper testOutputHelper)
        {
            _hashServiceMock = new Mock<ITileHashService>();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Sink(new TestOutputHelperSink(testOutputHelper))
                .CreateLogger();

            _tilesetTileFactory = new TilesetTileFactory(_hashServiceMock.Object, logger);
        }

        [Fact]
        public void FromFrames_ShouldReturnEmptyList_WhenFramesAreEmpty()
        {
            // Arrange
            var frames = new List<Image<Rgba32>>();
            var tileSize = new Size(16, 16);

            // Act
            var result = _tilesetTileFactory.FromFrames(frames, tileSize);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FromFrames_ShouldReturnOneTile_WhenFrameHasOneTile()
        {
            // Arrange
            var frames = new List<Image<Rgba32>>
            {
                new(16, 16)
                {
                    [0, 0] = Rgba32.ParseHex("FF0000"),
                    [15, 0] = Rgba32.ParseHex("0000FF")
                }
            };

            var tileSize = new Size(16, 16);
            var expectedHash = 123;
            _hashServiceMock.Setup(h => h.Compute(It.IsAny<Image<Rgba32>>(), It.IsAny<Size>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedHash);

            // Act
            var result = _tilesetTileFactory.FromFrames(frames, tileSize);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(new Point(0, 0), result[0].Location);
            Assert.Equal(expectedHash, result[0].Hash);

            var base64Expected = frames[0].ToBase64String(PngFormat.Instance);
            var base64Actual = result[0].Image?.ToBase64String(PngFormat.Instance);
            Assert.Equal(base64Expected, base64Actual);
        }

        [Fact]
        public void FromFrames_ShouldReturnTwoTiles_WhenFrameHasTwoDifferentTiles()
        {
            // Arrange
            var frames = new List<Image<Rgba32>>
            {
                new(32, 16)
                {
                    [0, 0] = Rgba32.ParseHex("FF0000"),
                    [16, 0] = Rgba32.ParseHex("0000FF")
                }
            };
            var tileSize = new Size(16, 16);
            var expectedHash1 = 123;
            var expectedHash2 = 456;
            _hashServiceMock.SetupSequence(h => h.Compute(It.IsAny<Image<Rgba32>>(), It.IsAny<Size>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(expectedHash1)
                .Returns(expectedHash2);

            // Act
            var result = _tilesetTileFactory.FromFrames(frames, tileSize);

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].Id);
            Assert.Equal(new Point(0, 0), result[0].Location);
            Assert.Equal(expectedHash1, result[0].Hash);

            Assert.Equal(2, result[1].Id);
            Assert.Equal(new Point(16, 0), result[1].Location);
            Assert.Equal(expectedHash2, result[1].Hash);
        }

        [Fact]
        public void FromFrames_ShouldReturnOneTile_WhenFrameHasTwoIdenticalTiles()
        {
            // Arrange
            var frames = new List<Image<Rgba32>>
            {
                new(32, 16)
                {
                    [0, 0] = Rgba32.ParseHex("FF0000"),
                    [16, 0] = Rgba32.ParseHex("FF0000")
                }
            };

            var tileSize = new Size(16, 16);
            var expectedHash = 123;

            _hashServiceMock.Setup(h => h.Compute(It.IsAny<Image<Rgba32>>(), It.IsAny<Size>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(expectedHash);

            // Act
            var result = _tilesetTileFactory.FromFrames(frames, tileSize);

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(new Point(0, 0), result[0].Location);
            Assert.Equal(expectedHash, result[0].Hash);
        }
    }
}
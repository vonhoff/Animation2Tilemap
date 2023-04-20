using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Test.Services
{
    public class TilesetHashServiceTests
    {
        private readonly ITileHashService _tileHashService;

        public TilesetHashServiceTests()
        {
            _tileHashService = new TileHashService();
        }

        [Fact]
        public void Compute_ReturnsNonZeroHash_ForValidImageAndTileSize()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var tileSize = new Size(10, 10);
            var x = 0;
            var y = 0;

            // Act
            var hash = _tileHashService.Compute(image, tileSize, x, y);

            // Assert
            Assert.NotEqual(0, hash);
        }

        [Fact]
        public void Compute_ReturnsSameHash_ForSameInput()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var tileSize = new Size(10, 10);
            var x = 0;
            var y = 0;

            // Act
            var hash1 = _tileHashService.Compute(image, tileSize, x, y);
            var hash2 = _tileHashService.Compute(image, tileSize, x, y);

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void Compute_ThrowsArgumentNullException_ForNullImage()
        {
            // Arrange
            Image<Rgba32>? image = null;
            var tileSize = new Size(10, 10);
            var x = 0;
            var y = 0;

            // Act + Assert
            Assert.Throws<NullReferenceException>(() => _tileHashService.Compute(image, tileSize, x, y));
        }

        [Fact]
        public void Compute_ThrowsArgumentOutOfRangeException_ForOutOfBoundsTileCoords()
        {
            // Arrange
            var image = new Image<Rgba32>(100, 100);
            var tileSize = new Size(10, 10);
            var x = 100; // out of bounds
            var y = 100; // out of bounds

            // Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _tileHashService.Compute(image, tileSize, x, y));
        }
    }
}
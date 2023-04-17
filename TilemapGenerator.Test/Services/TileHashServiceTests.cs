using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Test.Services
{
    public class TileHashServiceTests
    {
        private readonly ITileHashService _tileHashService;

        public TileHashServiceTests()
        {
            _tileHashService = new TileHashService();
        }

        [Fact]
        public void Compute_ReturnsDifferentValues_ForDifferentPixelColors()
        {
            // Arrange
            var pixelColor1 = new Rgba32(255, 0, 0, 255); // Red
            var pixelColor2 = new Rgba32(0, 255, 0, 255); // Green
            const int baseValue = 123;
            const int tileX = 2;
            const int tileY = 3;

            // Act
            var hash1 = _tileHashService.Compute(pixelColor1, baseValue, tileX, tileY);
            var hash2 = _tileHashService.Compute(pixelColor2, baseValue, tileX, tileY);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void Compute_ReturnsSameValue_ForSamePixelColor()
        {
            // Arrange
            var pixelColor = new Rgba32(255, 0, 0, 255); // Red
            const int baseValue = 123;
            const int tileX = 1;
            const int tileY = 1;

            // Act
            var hash1 = _tileHashService.Compute(pixelColor, baseValue, tileX, tileY);
            var hash2 = _tileHashService.Compute(pixelColor, baseValue, tileX, tileY);

            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void Compute_ReturnsDifferentValues_ForDifferentBaseValues()
        {
            // Arrange
            var pixelColor = new Rgba32(255, 0, 0, 255); // Red
            const int baseValue1 = 123;
            const int baseValue2 = 456;
            const int tileX = 1;
            const int tileY = 1;

            // Act
            var hash1 = _tileHashService.Compute(pixelColor, baseValue1, tileX, tileY);
            var hash2 = _tileHashService.Compute(pixelColor, baseValue2, tileX, tileY);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void Compute_ReturnsDifferentValues_ForDifferentTileCoordinates()
        {
            // Arrange
            var pixelColor = new Rgba32(255, 0, 0, 255); // Red
            const int baseValue = 123;
            const int tileX1 = 4;
            const int tileX2 = 1;
            const int tileY1 = 4;
            const int tileY2 = 1;

            // Act
            var hash1 = _tileHashService.Compute(pixelColor, baseValue, tileX1, tileY1);
            var hash2 = _tileHashService.Compute(pixelColor, baseValue, tileX2, tileY2);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }
    }
}
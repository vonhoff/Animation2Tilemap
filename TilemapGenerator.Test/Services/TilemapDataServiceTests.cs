using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Test.Services
{
    public class TilemapDataServiceTests
    {
        private readonly ITilemapDataService _tilemapDataService;
        private readonly string _base64WithZlib;
        private readonly string _base64WithGzip;
        private readonly int[] _tilemapData;
        private readonly byte[] _tilemapRotationData;

        public TilemapDataServiceTests()
        {
            _tilemapDataService = new TilemapDataService();
            _base64WithZlib = TestResourcesHelper.ImportText("Base64Zlib.txt");
            _base64WithGzip = TestResourcesHelper.ImportText("Base64Gzip.txt");
            _tilemapData = TestResourcesHelper.ImportArray<int>("Tilemap.json");
            _tilemapRotationData = TestResourcesHelper.ImportArray<byte>("TilemapRotations.json");
        }

        [Fact]
        public void ParseDataAsBase64_ShouldHaveCorrectResult_WhenUsingZlib()
        {
            // Arrange
            var dataFromZlib = Array.Empty<int>();
            var rotationFlags = Array.Empty<byte>();

            // Act
            _tilemapDataService.ParseDataAsBase64(_base64WithZlib, "zlib", ref dataFromZlib, ref rotationFlags);

            // Assert
            Assert.Equal(dataFromZlib, _tilemapData);
        }

        [Fact]
        public void ParseDataAsBase64_ShouldHaveCorrectResult_WhenUsingGzip()
        {
            // Arrange
            var dataFromGzip = Array.Empty<int>();
            var rotationFlags = Array.Empty<byte>();

            // Act
            _tilemapDataService.ParseDataAsBase64(_base64WithGzip, "gzip", ref dataFromGzip, ref rotationFlags);

            // Assert
            Assert.Equal(dataFromGzip, _tilemapData);
        }

        [Fact]
        public void SerializeDataAsBase64_ShouldHaveCorrectResult_WhenUsingZlib()
        {
            // Act
            var result = _tilemapDataService.SerializeDataAsBase64(_tilemapData, _tilemapRotationData, "zlib");

            // Assert
            Assert.Equal(_base64WithZlib, result);
        }
    }
}
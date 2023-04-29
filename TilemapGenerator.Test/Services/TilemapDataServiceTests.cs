using TilemapGenerator.Enums;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Test.Services
{
    public class TilemapDataServiceTests
    {
        private readonly ITilemapDataService _tilemapDataService;
        private readonly string _base64WithZlib;
        private readonly string _base64WithGzip;
        private readonly List<uint> _tilemapData;

        public TilemapDataServiceTests()
        {
            _tilemapDataService = new TilemapDataService();
            _base64WithZlib = TestResourcesHelper.ImportText("Base64Zlib.txt");
            _base64WithGzip = TestResourcesHelper.ImportText("Base64Gzip.txt");
            _tilemapData = new List<uint>(TestResourcesHelper.ImportArray<uint>("Tilemap.json"));
        }

        [Fact]
        public void ParseDataAsBase64_ShouldHaveCorrectResult_WhenUsingZlib()
        {
            // Act
            var tilemapData = _tilemapDataService.ParseDataAsBase64(_base64WithZlib, TilemapDataCompression.ZLib);

            // Assert
            Assert.Equal(_tilemapData, tilemapData);
        }

        [Fact]
        public void ParseDataAsBase64_ShouldHaveCorrectResult_WhenUsingGzip()
        {
            // Act
            var tilemapData = _tilemapDataService.ParseDataAsBase64(_base64WithGzip, TilemapDataCompression.GZip);

            // Assert
            Assert.Equal(_tilemapData, tilemapData);
        }

        [Fact]
        public void SerializeDataAsBase64_ShouldHaveCorrectResult_WhenUsingZlib()
        {
            // Act
            var serialized = _tilemapDataService.SerializeDataAsBase64(TilemapDataCompression.ZLib, _tilemapData);
            var tilemapData = _tilemapDataService.ParseDataAsBase64(serialized, TilemapDataCompression.ZLib);

            // Assert
            Assert.Equal(_tilemapData, tilemapData);
        }

        [Fact]
        public void SerializeDataAsBase64_ShouldHaveCorrectResult_WhenUsingGzip()
        {
            // Act
            var serialized = _tilemapDataService.SerializeDataAsBase64(TilemapDataCompression.GZip, _tilemapData);
            var tilemapData = _tilemapDataService.ParseDataAsBase64(serialized, TilemapDataCompression.GZip);

            // Assert
            Assert.Equal(_tilemapData, tilemapData);
        }
    }
}
using Animation2Tilemap.Entities;
using Animation2Tilemap.Enums;
using Animation2Tilemap.Factories;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Workflows;
using Moq;
using SixLabors.ImageSharp;

namespace Animation2Tilemap.Test.Factories;

public class TilemapFactoryTests
{
    private readonly TilemapFactory _factory;
    private readonly Mock<ITilemapDataService> _tilemapDataServiceMock;
    private readonly MainWorkflowOptions _options;

    public TilemapFactoryTests()
    {
        _options = new MainWorkflowOptions
        {
            TileLayerFormat = TileLayerFormat.Csv
        };

        _tilemapDataServiceMock = new Mock<ITilemapDataService>();
        _factory = new TilemapFactory(_options, _tilemapDataServiceMock.Object);
    }

    [Fact]
    public void CreateFromTileset_WithBasicTileset_CreatesCorrectTilemap()
    {
        // Arrange
        var tileset = new Tileset
        {
            Name = "test",
            TileWidth = 32,
            TileHeight = 32,
            OriginalSize = new Size(64, 64),
            RegisteredTiles = new List<TilesetTile>
            {
                new() { Id = 0, Animation = new TilesetTileAnimation { Hash = 1u } },
                new() { Id = 1, Animation = new TilesetTileAnimation { Hash = 2u } }
            },
            HashAccumulations = new Dictionary<Point, uint>
            {
                { new Point(0, 0), 1u },
                { new Point(32, 0), 2u }
            }
        };

        var frameTimes = new List<int> { 100, 200 };

        _tilemapDataServiceMock
            .Setup(x => x.SerializeData(It.IsAny<uint[]>(), TileLayerFormat.Csv))
            .Returns("1,2");

        // Act
        var result = _factory.CreateFromTileset(tileset, frameTimes);

        // Assert
        Assert.Equal("1.0", result.Version);
        Assert.Equal("orthogonal", result.Orientation);
        Assert.Equal("right-down", result.RenderOrder);
        Assert.Equal(2, result.Width);
        Assert.Equal(2, result.Height);
        Assert.Equal(32, result.TileWidth);
        Assert.Equal(32, result.TileHeight);
        Assert.Equal(1, result.NextObjectId);
        Assert.NotNull(result.Tileset);
        Assert.Equal("test.tsx", result.Tileset.Source);
        Assert.NotNull(result.TilemapLayer?.Data);
        Assert.Equal("csv", result.TilemapLayer.Data.Encoding);
        Assert.Null(result.TilemapLayer.Data.Compression);
        Assert.Equal("1,2", result.TilemapLayer.Data.Text);

        // Verify frame timing information was added to tileset properties
        Assert.NotNull(tileset.Properties);
        var frameTimesProperty = tileset.Properties.FirstOrDefault(p => p.Name == "frameTimes");
        Assert.NotNull(frameTimesProperty);
        Assert.Equal("string", frameTimesProperty.Type);
        Assert.Equal("100,200", frameTimesProperty.Value);

        _tilemapDataServiceMock.Verify(
            x => x.SerializeData(
                It.Is<uint[]>(arr => arr[0] == 1 && arr[1] == 2),
                TileLayerFormat.Csv),
            Times.Once);
    }

    [Theory]
    [InlineData(TileLayerFormat.Base64Uncompressed, "base64", null)]
    [InlineData(TileLayerFormat.Base64GZip, "base64", "gzip")]
    [InlineData(TileLayerFormat.Base64ZLib, "base64", "zlib")]
    [InlineData(TileLayerFormat.Csv, "csv", null)]
    public void CreateFromTileset_WithDifferentFormats_UsesCorrectEncoding(
        TileLayerFormat format,
        string expectedEncoding,
        string? expectedCompression)
    {
        // Arrange
        _options.TileLayerFormat = format;
        var factory = new TilemapFactory(_options, _tilemapDataServiceMock.Object);

        var tileset = new Tileset
        {
            Name = "test",
            TileWidth = 32,
            TileHeight = 32,
            OriginalSize = new Size(32, 32),
            RegisteredTiles = new List<TilesetTile>(),
            HashAccumulations = new Dictionary<Point, uint>
            {
                { new Point(0, 0), 1u }
            }
        };

        var frameTimes = new List<int> { 100 };

        _tilemapDataServiceMock
            .Setup(x => x.SerializeData(It.IsAny<uint[]>(), format))
            .Returns("data");

        // Act
        var result = factory.CreateFromTileset(tileset, frameTimes);

        // Assert
        Assert.NotNull(result.TilemapLayer?.Data);
        Assert.Equal(expectedEncoding, result.TilemapLayer.Data.Encoding);
        Assert.Equal(expectedCompression, result.TilemapLayer.Data.Compression);

        // Verify frame timing information was added to tileset properties
        Assert.NotNull(tileset.Properties);
        var frameTimesProperty = tileset.Properties.FirstOrDefault(p => p.Name == "frameTimes");
        Assert.NotNull(frameTimesProperty);
        Assert.Equal("string", frameTimesProperty.Type);
        Assert.Equal("100", frameTimesProperty.Value);
    }

    [Fact]
    public void CreateFromTileset_WithEmptyFrameTimes_DoesNotAddFrameTimesProperty()
    {
        // Arrange
        var tileset = new Tileset
        {
            Name = "test",
            TileWidth = 32,
            TileHeight = 32,
            OriginalSize = new Size(32, 32),
            RegisteredTiles = new List<TilesetTile>(),
            HashAccumulations = new Dictionary<Point, uint>
            {
                { new Point(0, 0), 1u }
            }
        };

        var frameTimes = new List<int>();

        _tilemapDataServiceMock
            .Setup(x => x.SerializeData(It.IsAny<uint[]>(), TileLayerFormat.Csv))
            .Returns("1");

        // Act
        var result = _factory.CreateFromTileset(tileset, frameTimes);

        // Assert
        Assert.Null(tileset.Properties);
    }
} 
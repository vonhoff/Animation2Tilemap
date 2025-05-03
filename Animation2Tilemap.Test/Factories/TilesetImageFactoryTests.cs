using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories;
using Animation2Tilemap.Workflows;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Test.Factories;

public class TilesetImageFactoryTests
{
    private readonly TilesetImageFactory _factory;
    private readonly MainWorkflowOptions _options;

    public TilesetImageFactoryTests()
    {
        _options = new MainWorkflowOptions
        {
            TileMargin = 1,
            TileSize = new Size(32, 32),
            TileSpacing = 1,
            TransparentColor = new Rgba32(255, 0, 255)
        };

        _factory = new TilesetImageFactory(_options);
    }

    [Fact]
    public void CreateFromTiles_WithSingleTile_CreatesCorrectImage()
    {
        // Arrange
        var testImage = new Image<Rgba32>(32, 32);
        var tile = new TilesetTile
        {
            Id = 0,
            Image = new TilesetTileImage(testImage, 0)
        };
        var tiles = new List<TilesetTile>
        {
            tile
        };

        // Act
        var result = _factory.CreateFromTiles(tiles, "test.png");

        // Assert
        Assert.Equal("test.png", result.Path);
        Assert.Equal(_options.TransparentColor.ToHex(), result.Trans);
        Assert.Equal(33, result.Width);
        Assert.Equal(33, result.Height);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void CreateFromTiles_WithMultipleTiles_CreatesCorrectLayout()
    {
        // Arrange
        var testImage1 = new Image<Rgba32>(32, 32);
        var testImage2 = new Image<Rgba32>(32, 32);
        var testImage3 = new Image<Rgba32>(32, 32);
        var testImage4 = new Image<Rgba32>(32, 32);

        var tiles = new List<TilesetTile>
        {
            new()
            {
                Id = 0,
                Image = new TilesetTileImage(testImage1, 0)
            },
            new()
            {
                Id = 1,
                Image = new TilesetTileImage(testImage2, 1)
            },
            new()
            {
                Id = 2,
                Image = new TilesetTileImage(testImage3, 2)
            },
            new()
            {
                Id = 3,
                Image = new TilesetTileImage(testImage4, 3)
            }
        };

        // Act
        var result = _factory.CreateFromTiles(tiles, "test.png");

        // Assert
        Assert.Equal(66, result.Width);
        Assert.Equal(66, result.Height);
        Assert.NotNull(result.Data);
    }
}
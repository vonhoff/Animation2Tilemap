using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Workflows;
using Moq;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Test.Factories;

public class TilesetFactoryTests
{
    private readonly TilesetFactory _factory;
    private readonly Mock<IImageHashService> _imageHashServiceMock;
    private readonly Mock<ITilesetImageFactory> _tilesetImageFactoryMock;

    public TilesetFactoryTests()
    {
        var options = new MainWorkflowOptions
        {
            Fps = 60,
            TileMargin = 1,
            TileSize = new Size(32, 32),
            TileSpacing = 1
        };

        _tilesetImageFactoryMock = new Mock<ITilesetImageFactory>();
        _imageHashServiceMock = new Mock<IImageHashService>();
        var loggerMock = new Mock<ILogger>();

        _factory = new TilesetFactory(
            options,
            _tilesetImageFactoryMock.Object,
            _imageHashServiceMock.Object,
            loggerMock.Object);
    }

    [Fact]
    public void CreateFromImage_WithSingleFrame_CreatesTileset()
    {
        // Arrange
        var frame = new Image<Rgba32>(64, 64);
        var frames = new List<Image<Rgba32>> { frame };
        const string fileName = "test.png";

        var hashCalls = 0;
        _imageHashServiceMock
            .Setup(x => x.Compute(It.IsAny<Image<Rgba32>>()))
            .Returns(() => (uint)++hashCalls); // Return incrementing hash values

        var expectedTilesetImage = new TilesetImage
        {
            Path = fileName,
            Width = 34,
            Height = 34
        };

        _tilesetImageFactoryMock
            .Setup(x => x.CreateFromTiles(It.IsAny<IReadOnlyList<TilesetTile>>(), fileName))
            .Returns(expectedTilesetImage);

        // Act
        var result = _factory.CreateFromImage(fileName, frames);

        // Assert
        Assert.Equal(fileName, result.Name);
        Assert.Equal(32, result.TileWidth);
        Assert.Equal(32, result.TileHeight);
        Assert.Equal(1, result.Margin);
        Assert.Equal(1, result.Spacing);
        Assert.Equal(4, result.TileCount);
        Assert.Equal(1, result.Columns);
        Assert.Equal(expectedTilesetImage, result.Image);
        Assert.Empty(result.AnimatedTiles);
        Assert.Equal(4, result.RegisteredTiles.Count);
        Assert.Equal(4, result.HashAccumulations.Count);
        Assert.Equal(frame.Size, result.OriginalSize);

        _imageHashServiceMock.Verify(
            x => x.Compute(It.IsAny<Image<Rgba32>>()),
            Times.Exactly(4));
    }

    [Fact]
    public void CreateFromImage_WithMultipleFrames_CreatesAnimatedTileset()
    {
        // Arrange
        var frame1 = new Image<Rgba32>(32, 32);
        var frame2 = new Image<Rgba32>(32, 32);
        var frames = new List<Image<Rgba32>> { frame1, frame2 };
        const string fileName = "test.png";

        _imageHashServiceMock
            .SetupSequence(x => x.Compute(It.IsAny<Image<Rgba32>>()))
            .Returns(1u)
            .Returns(2u);

        var expectedTilesetImage = new TilesetImage
        {
            Path = fileName,
            Width = 68,
            Height = 34
        };

        _tilesetImageFactoryMock
            .Setup(x => x.CreateFromTiles(It.IsAny<IReadOnlyList<TilesetTile>>(), fileName))
            .Returns(expectedTilesetImage);

        // Act
        var result = _factory.CreateFromImage(fileName, frames);

        // Assert
        Assert.Equal(2, result.TileCount);
        Assert.Single(result.AnimatedTiles);
        Assert.Equal(2, result.RegisteredTiles.Count);
        Assert.Single(result.HashAccumulations);

        var animatedTile = result.AnimatedTiles[0];
        Assert.Equal(2, animatedTile.Animation!.Frames.Count);
        Assert.Equal(16, animatedTile.Animation.Frames[0].Duration);
        Assert.Equal(16, animatedTile.Animation.Frames[1].Duration);
    }

    [Fact]
    public void CreateFromImage_WithIdenticalFrames_OptimizesAnimation()
    {
        // Arrange
        var frame1 = new Image<Rgba32>(32, 32);
        var frame2 = new Image<Rgba32>(32, 32);
        var frame3 = new Image<Rgba32>(32, 32);
        var frames = new List<Image<Rgba32>> { frame1, frame2, frame3 };
        const string fileName = "test.png";

        _imageHashServiceMock
            .SetupSequence(x => x.Compute(It.IsAny<Image<Rgba32>>()))
            .Returns(1u)
            .Returns(1u)
            .Returns(2u);

        var expectedTilesetImage = new TilesetImage
        {
            Path = fileName,
            Width = 68,
            Height = 34
        };

        _tilesetImageFactoryMock
            .Setup(x => x.CreateFromTiles(It.IsAny<IReadOnlyList<TilesetTile>>(), fileName))
            .Returns(expectedTilesetImage);

        // Act
        var result = _factory.CreateFromImage(fileName, frames);

        // Assert
        Assert.Equal(2, result.TileCount);
        Assert.Single(result.AnimatedTiles);

        var animatedTile = result.AnimatedTiles[0];
        Assert.Equal(2, animatedTile.Animation!.Frames.Count);
        Assert.Equal(32, animatedTile.Animation.Frames[0].Duration);
        Assert.Equal(16, animatedTile.Animation.Frames[1].Duration);
    }
}
using System.Diagnostics;
using Serilog;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilesetFactory : ITilesetFactory
{
    private readonly int _frameDuration;
    private readonly IImageHashService _imageHashService;
    private readonly ILogger _logger;
    private readonly ITilesetImageFactory _tilesetImageFactory;
    private readonly Size _tileSize;

    public TilesetFactory(
        ApplicationOptions options,
        ITilesetImageFactory tilesetImageFactory,
        IImageHashService imageHashService,
        ILogger logger)
    {
        _imageHashService = imageHashService;
        _tilesetImageFactory = tilesetImageFactory;
        _logger = logger;
        _tileSize = options.TileSize;
        _frameDuration = options.FrameDuration;
    }

    /// <summary>
    /// Creates a new tileset from an image file and a list of frames.
    /// </summary>
    /// <param name="fileName">The name of the image file used to create the tileset.</param>
    /// <param name="frames">The list of frames used to create the tileset.</param>
    /// <returns>A new Tileset object created from the provided image file and frames.</returns>
    public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
    {
        var tileImages = new Dictionary<Point, List<TilesetTileImage>>();
        var tileHashAccumulations = new Dictionary<Point, int>();
        var registeredTiles = new List<TilesetTile>();
        var animationDuration = _frameDuration * frames.Count;
        var stopwatch = Stopwatch.StartNew();

        foreach (var frame in frames)
        {
            UpdateTileHashAccumulations(frame, tileHashAccumulations, tileImages);
        }

        foreach (var hashAccumulation in tileHashAccumulations.DistinctBy(a => a.Value))
        {
            CreateTilesFromCollection(tileImages, hashAccumulation, registeredTiles, animationDuration);
        }

        var animationTiles = registeredTiles.Where(t => t.Animation is { Frames.Count: > 1 }).ToList();
        stopwatch.Stop();

        _logger.Verbose("Registered {HashCount} distinct tile(s) and {AnimationCount} tile animation(s) for {FileName}. Took: {Elapsed}ms",
            registeredTiles.Count, animationTiles.Count, fileName, stopwatch.ElapsedMilliseconds);

        var tilesetImage = _tilesetImageFactory.CreateFromTiles(registeredTiles, fileName);
        var tileset = new Tileset
        {
            Name = fileName,
            TileWidth = _tileSize.Width,
            TileHeight = _tileSize.Height,
            TileCount = registeredTiles.Count,
            Columns = tilesetImage.Width / _tileSize.Width,
            Image = tilesetImage,
            AnimatedTiles = animationTiles,
            RegisteredTiles = registeredTiles,
            HashAccumulations = tileHashAccumulations,
            OriginalSize = frames[0].Size
        };

        return tileset;
    }

    /// <summary>
    /// Adds an animation frame to the specified tile.
    /// </summary>
    /// <param name="tile">The tile to add the animation frame to.</param>
    /// <param name="registeredTiles">The collection of registered tiles to add the new tile to.</param>
    /// <param name="tileImage">The tile image to add to the animation.</param>
    /// <param name="duration">The duration of the animation frame.</param>
    private static void AddAnimationFrame(TilesetTile tile, ICollection<TilesetTile> registeredTiles, TilesetTileImage tileImage, int duration)
    {
        var registeredTile = registeredTiles.FirstOrDefault(t => t.Image.Equals(tileImage));
        if (registeredTile == null)
        {
            registeredTile = new TilesetTile
            {
                Id = registeredTiles.Count,
                Image = tileImage
            };
            registeredTiles.Add(registeredTile);
        }
        var tileAnimationFrame = new TilesetTileAnimationFrame
        {
            TileId = registeredTile.Id,
            Duration = duration
        };
        tile.Animation?.Frames.Add(tileAnimationFrame);
    }

    /// <summary>
    /// Creates tiles from a collection of tile images with the same hash accumulation value and adds them to the specified collection of registered tiles.
    /// </summary>
    /// <param name="tileImages">The dictionary of tile images.</param>
    /// <param name="hashAccumulation">The key-value pair containing the hash accumulation value and its associated point.</param>
    /// <param name="registeredTiles">The collection of registered tiles to add the new tiles to.</param>
    /// <param name="animationDuration">The duration of the animation.</param>
    private void CreateTilesFromCollection(
        IReadOnlyDictionary<Point, List<TilesetTileImage>> tileImages,
        KeyValuePair<Point, int> hashAccumulation,
        ICollection<TilesetTile> registeredTiles,
        int animationDuration)
    {
        var tileImageCollection = tileImages[hashAccumulation.Key];
        var tile = new TilesetTile
        {
            Id = registeredTiles.Count,
            Image = tileImageCollection[0],
            Animation = new TilesetTileAnimation
            {
                Frames = new List<TilesetTileAnimationFrame>(),
                Hash = hashAccumulation.Value
            }
        };
        registeredTiles.Add(tile);
        var previousTileImage = tileImageCollection[0];
        var previousFrameDuration = 0;
        foreach (var currentTileImage in tileImageCollection)
        {
            if (currentTileImage.Equals(previousTileImage))
            {
                previousFrameDuration += _frameDuration;
            }
            else
            {
                AddAnimationFrame(tile, registeredTiles, previousTileImage, previousFrameDuration);
                previousTileImage = currentTileImage;
                previousFrameDuration = _frameDuration;
            }
        }
        AddAnimationFrame(tile, registeredTiles, previousTileImage, animationDuration - tile.Animation.Frames.Sum(f => f.Duration));
    }

    /// <summary>
    /// Generates a sequence of tile locations for a given width and height.
    /// </summary>
    /// <param name="width">The width of the tileset image.</param>
    /// <param name="height">The height of the tileset image.</param>
    /// <returns>An enumerable collection of points representing tile locations.</returns>
    private IEnumerable<Point> GetTileLocations(int width, int height)
    {
        for (var y = 0; y < height; y += _tileSize.Height)
        {
            for (var x = 0; x < width; x += _tileSize.Width)
            {
                yield return new Point(x, y);
            }
        }
    }

    /// <summary>
    /// Updates the hash accumulations for each tile location in the specified image and adds tile images to their corresponding tile collections.
    /// </summary>
    /// <param name="frame">The image to update the tile hash accumulations for.</param>
    /// <param name="hashAccumulations">A dictionary that maps tile locations to their hash accumulations.</param>
    /// <param name="tileImages">A dictionary that maps tile locations to their corresponding tile images.</param>
    /// <remarks>
    /// The method divides the image into tiles of the specified size and computes the hash for each tile image.
    /// It then updates the hash accumulation for each tile location and adds the tile image to the tile collection for the tile location.
    /// </remarks>
    private void UpdateTileHashAccumulations(
        Image<Rgba32> frame,
        IDictionary<Point, int> hashAccumulations,
        IDictionary<Point, List<TilesetTileImage>> tileImages)
    {
        var tileLocations = GetTileLocations(frame.Width, frame.Height);
        foreach (var tileLocation in tileLocations)
        {
            var tileBounds = new Rectangle(tileLocation, _tileSize);
            var tileFrame = frame.Clone(ctx => ctx.Crop(tileBounds));
            var tileHash = _imageHashService.Compute(tileFrame);
            var tileImage = new TilesetTileImage(tileFrame, tileHash);

            hashAccumulations[tileLocation] = hashAccumulations.TryGetValue(tileLocation, out var accumulation)
                ? (accumulation * 33) ^ tileHash
                : tileHash;

            if (!tileImages.TryGetValue(tileLocation, out var locationImages))
            {
                locationImages = new List<TilesetTileImage>();
                tileImages.Add(tileLocation, locationImages);
            }
            locationImages.Add(tileImage);
        }
    }
}
using System.Diagnostics;
using Serilog;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilesetFactory : ITilesetFactory
{
    private readonly ITilesetImageFactory _tilesetImageFactory;
    private readonly IImageHashService _imageHashService;
    private readonly ILogger _logger;
    private readonly Size _tileSize;
    private readonly int _frameDuration;

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
    /// Creates a tileset from the given image file and list of frames.
    /// The method generates all unique tiles and animations based on the frames.
    /// </summary>
    /// <param name="fileName">The name of the file from which the tileset image was loaded.</param>
    /// <param name="frames">The list of frames used to generate the tileset tiles and animations.</param>
    /// <returns>A new Tileset object containing the generated tileset data.</returns>
    public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
    {
        var tileCollections = new Dictionary<Point, List<TilesetTileImage>>();
        var tileHashAccumulations = new Dictionary<Point, int>();
        var registeredTiles = new List<TilesetTile>();
        var animationDuration = _frameDuration * frames.Count;
        var stopwatch = Stopwatch.StartNew();

        foreach (var frame in frames)
        {
            UpdateTileHashAccumulations(frame, tileHashAccumulations, tileCollections);
        }
        
        foreach (var hashAccumulation in tileHashAccumulations.DistinctBy(a => a.Value))
        {
            CreateTilesFromCollection(tileCollections, hashAccumulation, registeredTiles, animationDuration);
        }

        var animationTiles = registeredTiles.Where(t => t.Animation is { Frames.Count: > 1 }).ToList();
        stopwatch.Stop();

        _logger.Information("Registered {HashCount} distinct tile(s) and {AnimationCount} tile animation(s) for {FileName}. Took: {Elapsed}ms",
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
    /// Creates tiles from the tile image collection at the given location and adds them to the tile register.
    /// </summary>
    /// <param name="tileCollections">The dictionary of tile image collections.</param>
    /// <param name="hashAccumulation">The hash accumulation and location of the tile image collection.</param>
    /// <param name="registeredTiles">The list of registered tiles.</param>
    /// <param name="animationDuration">The total animation duration in milliseconds.</param>
    private void CreateTilesFromCollection(
        IReadOnlyDictionary<Point, List<TilesetTileImage>> tileCollections,
        KeyValuePair<Point, int> hashAccumulation, 
        ICollection<TilesetTile> registeredTiles,
        int animationDuration)
    {
        // Collect all tile images at this location.
        var tileImageCollection = tileCollections[hashAccumulation.Key];

        // Create a new tile since we already used DistinctBy to filter out duplicate hash accumulations.
        var tile = new TilesetTile
        {
            Id = registeredTiles.Count,
            Image = tileImageCollection[0],
            Animation = new TilesetTileAnimation
            {
                Frames = new List<TilesetTileAnimationFrame>
                {
                    new()
                    {
                        TileId = registeredTiles.Count,
                        Duration = 0
                    }
                },
                Hash = hashAccumulation.Value
            }
        };

        // Add the tile to the tile register.
        registeredTiles.Add(tile);

        // Keep track of the previous tile image.
        var previousTileImage = tileImageCollection[0];

        // Iterate through the tile images at this location and determine the animation frames.
        for (var i = 0; i < tileImageCollection.Count; i++)
        {
            var isLastFrame = i == tileImageCollection.Count - 1;
            var remainingTime = animationDuration - i * _frameDuration;

            var currentTileImage = tileImageCollection[i];
            if (currentTileImage.Equals(previousTileImage))
            {
                // Here the image has not changed from the previous tile, just update the duration,
                // as the duration tells how long (in milliseconds) this frame should be displayed before moving on to the next frame.
                var lastTile = tile.Animation.Frames.Last();
                lastTile.Duration += _frameDuration;
            }
            else
            {
                // We have received a new tile, maybe this tile is already registered. If not, just create a new one.
                var registeredTile = registeredTiles.FirstOrDefault(t => t.Image.Equals(currentTileImage));
                if (registeredTile == null)
                {
                    registeredTile = new TilesetTile
                    {
                        Id = registeredTiles.Count,
                        Image = currentTileImage
                    };
                    registeredTiles.Add(registeredTile);
                }

                // Create a new animation frame as current tile image has changed from the previous tile image.
                var tileAnimationFrame = new TilesetTileAnimationFrame
                {
                    TileId = registeredTile.Id,
                    Duration = isLastFrame ? remainingTime : _frameDuration
                };
                tile.Animation.Frames.Add(tileAnimationFrame);
            }

            previousTileImage = currentTileImage;
        }
    }

    /// <summary>
    /// Computes the hash accumulations for each tile in a given image frame and updates the tile collections
    /// dictionary with the resulting tile data.
    /// </summary>
    /// <param name="frame">The image frame to process.</param>
    /// <param name="hashAccumulations">A dictionary of tile hash accumulations to update.</param>
    /// <param name="tileCollections">A dictionary of tile collections to update.</param>
    /// <remarks>
    /// This method takes an image frame and computes a hash accumulation value for each tile in the image.
    /// A hash accumulation is simply a combined hash of the hashes in a given tile collection.
    /// The resulting hash accumulation values are used to distinguish tile animations.
    /// </remarks>
    private void UpdateTileHashAccumulations(
        Image<Rgba32> frame,
        IDictionary<Point, int> hashAccumulations,
        IDictionary<Point, List<TilesetTileImage>> tileCollections)
    {
        for (var y = 0; y < frame.Height; y += _tileSize.Height)
        {
            for (var x = 0; x < frame.Width; x += _tileSize.Width)
            {
                var tileLocation = new Point(x, y);
                var tileBounds = new Rectangle(tileLocation, _tileSize);
                var tileFrame = frame.Clone(ctx => ctx.Crop(tileBounds));
                var tileHash = _imageHashService.Compute(tileFrame);
                var tileImage = new TilesetTileImage(tileFrame, tileHash);

                // Try to get the existing hash accumulation for this tile location.
                if (hashAccumulations.TryGetValue(tileLocation, out var accumulation))
                {
                    // If it exists, update the accumulation.
                    accumulation = (accumulation * 33) ^ tileHash;
                    hashAccumulations[tileLocation] = accumulation;
                }
                else
                {
                    // If it doesn't exist, add a new entry to the hash accumulations dictionary.
                    hashAccumulations.Add(tileLocation, tileHash);
                }

                // Update the collection at this location.
                if (tileCollections.TryGetValue(tileLocation, out var tileCollection))
                {
                    tileCollection.Add(tileImage);
                }
                else
                {
                    tileCollections.Add(tileLocation, new List<TilesetTileImage> { tileImage });
                }
            }
        }
    }
}
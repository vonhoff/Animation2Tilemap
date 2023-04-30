using System.Diagnostics;
using Serilog;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilesetFactory : ITilesetFactory
{
    private readonly Size _tileSize;
    private readonly int _frameDuration;
    private readonly ITilesetImageFactory _tilesetImageFactory;
    private readonly IImageHashService _imageHashService;
    private readonly ILogger _logger;

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
    /// Creates a tileset from the given image file and list of frames. Each frame represents a single tileset tile,
    /// and the method generates all unique tiles and animations based on the frames.
    /// </summary>
    /// <param name="fileName">The name of the file from which the tileset image was loaded.</param>
    /// <param name="frames">The list of frames used to generate the tileset tiles and animations.</param>
    /// <returns>A new Tileset object containing the generated tileset data.</returns>
    /// <remarks>
    /// This method generates unique tiles based on the hash value of each tile's image. Tiles with the same hash value are
    /// considered identical, and the method generates a single tile with multiple animation frames for all matching tiles.
    /// </remarks>
    public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
    {
        var tileCollections = new Dictionary<Point, List<TilesetTileImage>>();
        var tileHashAccumulations = new Dictionary<Point, int>();
        var registeredTiles = new List<TilesetTile>();
        var animationDuration = _frameDuration * frames.Count;

        var stopwatch = Stopwatch.StartNew();
        foreach (var frame in frames)
        {
            ComputeTileHashAccumulations(frame, tileHashAccumulations, tileCollections);
        }

        var distinctHashAccumulations = tileHashAccumulations.DistinctBy(a => a.Value).ToList();
        _logger.Verbose("Computed {hashCount} distinct hash accumulation(s) for {fileName}. Took: {elapsed}ms",
            distinctHashAccumulations.Count, fileName, stopwatch.ElapsedMilliseconds);

        stopwatch.Restart();
        foreach (var hashAccumulation in distinctHashAccumulations)
        {
            CreateTilesFromCollection(tileCollections, hashAccumulation, registeredTiles, animationDuration);
        }
        _logger.Verbose("Registered {hashCount} distinct tile(s) for {fileName}. Took: {elapsed}ms",
            registeredTiles.Count, fileName, stopwatch.ElapsedMilliseconds);

        stopwatch.Restart();
        var animationTiles = registeredTiles.Where(t => t.Animation is { Frames.Count: > 1 }).ToList();
        _logger.Verbose("Registered {hashCount} animation(s) for {fileName}. Took: {elapsed}ms",
            animationTiles.Count, fileName, stopwatch.ElapsedMilliseconds);

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
            HashAccumulations = tileHashAccumulations
        };

        return tileset;
    }

    /// <summary>
    /// Creates tiles from the tile image collection at the given location and adds them to the tile register.
    /// </summary>
    /// <param name="tileCollections">The dictionary of tile image collections.</param>
    /// <param name="hashAccumulation">The hash accumulation and location of the tile image collection.</param>
    /// <param name="tileRegister">The list of registered tiles.</param>
    /// <param name="animationDuration">The total animation duration in milliseconds.</param>
    /// <remarks>
    /// <para>
    /// This method creates tiles from the tile image collection at the given location and adds them to the tile register.
    /// The method checks if the tile image collection contains any duplicates, filters them out, and creates a new tile.
    /// The new tile is then added to the tile register along with its animation frames.
    /// </para>
    /// <para>
    /// If the tile image collection contains more than one unique image, the method creates new tiles for each unique image
    /// and adds them to the tile register. The method then updates the animation frames of the original tile with the newly created tiles.
    /// </para>
    /// </remarks>
    private void CreateTilesFromCollection(
        IReadOnlyDictionary<Point, List<TilesetTileImage>> tileCollections,
        KeyValuePair<Point, int> hashAccumulation, List<TilesetTile> tileRegister,
        int animationDuration)
    {
        // Collect all tile images at this location.
        var tileImageCollection = tileCollections[hashAccumulation.Key];

        // Create a new tile since we already used DistinctBy to filter out duplicate hash accumulations.
        var tile = new TilesetTile
        {
            Id = tileRegister.Count,
            Image = tileImageCollection[0],
            Animation = new TilesetTileAnimation
            {
                Frames = new List<TilesetTileAnimationFrame>
                {
                    new()
                    {
                        TileId = tileRegister.Count,
                        Duration = 0
                    }
                },
                Hash = hashAccumulation.Value
            }
        };

        // Add the tile to the tile register.
        tileRegister.Add(tile);

        // Keep track of the previous tile image to comparison.
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
                var registeredTile = tileRegister.Find(t => t.Image.Equals(currentTileImage));
                if (registeredTile == null)
                {
                    registeredTile = new TilesetTile
                    {
                        Id = tileRegister.Count,
                        Image = currentTileImage
                    };
                    tileRegister.Add(registeredTile);
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
    /// <para>
    /// This method takes an image frame and computes a hash accumulation value for each tile in the image.
    /// A hash accumulation is simply a sum of the RGB values of all pixels in a given tile. The resulting
    /// hash accumulation values are used to group tiles together that have the same appearance.
    /// </para>
    /// <para>
    /// The tile hash accumulations and associated tile images are stored in the tileHashAccumulations and
    /// tileCollections dictionaries, respectively. Each tile collection is a list of TilesetTileImage objects,
    /// which contain the image data for a single tile in the image.
    /// </para>
    /// </remarks>
    private void ComputeTileHashAccumulations(
        Image<Rgba32> frame,
        IDictionary<Point, int> hashAccumulations,
        IDictionary<Point, List<TilesetTileImage>> tileCollections)
    {
        for (var x = 0; x < frame.Width; x += _tileSize.Width)
        {
            for (var y = 0; y < frame.Height; y += _tileSize.Height)
            {
                var tileLocation = new Point(x, y);
                var tileBounds = new Rectangle(tileLocation, _tileSize);
                var tileFrame = frame.Clone(ctx => ctx.Crop(tileBounds));
                var tileHash = _imageHashService.Compute(tileFrame);
                var tileImage = new TilesetTileImage(tileFrame, tileHash);

                // Accumulate the tile hash collection at this location.
                if (hashAccumulations.TryGetValue(tileLocation, out var accumulation))
                {
                    hashAccumulations[tileLocation] = ((accumulation << 5) + accumulation) ^ tileHash;
                }
                else
                {
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
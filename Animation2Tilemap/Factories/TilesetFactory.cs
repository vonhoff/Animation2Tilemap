using System.Diagnostics;
using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Workflows;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Animation2Tilemap.Factories;

public class TilesetFactory : ITilesetFactory
{
    private readonly ITilesetImageFactory _tilesetImageFactory;
    private readonly IImageHashService _imageHashService;
    private readonly ILogger _logger;
    private readonly int _frameDuration;
    private readonly int _tileMargin;
    private readonly Size _tileSize;
    private readonly int _tileSpacing;

    public TilesetFactory(MainWorkflowOptions options,
        ITilesetImageFactory tilesetImageFactory,
        IImageHashService imageHashService,
        ILogger logger)
    {
        _tilesetImageFactory = tilesetImageFactory;
        _imageHashService = imageHashService;
        _logger = logger;
        _frameDuration = options.FrameDuration;
        _tileMargin = options.TileMargin;
        _tileSize = options.TileSize;
        _tileSpacing = options.TileSpacing;
    }

    public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
    {
        var tileImages = new Dictionary<Point, List<TilesetTileImage>>();
        var hashAccumulations = new Dictionary<Point, uint>();
        var registeredTiles = new List<TilesetTile>();
        var imageTileMap = new Dictionary<int, TilesetTile>();
        var animationDuration = _frameDuration * frames.Count;
        var stopwatch = Stopwatch.StartNew();

        // Extract all tiles from each frame and gather their image data with computed hashes
        ExtractAndHashTiles(frames, tileImages, hashAccumulations);

        // Build tile animations by analyzing how each tile position changes across frames
        CreateTileAnimations(tileImages, hashAccumulations, registeredTiles, imageTileMap, animationDuration);

        var animationTiles = registeredTiles.Where(t => t.Animation is { Frames.Count: > 1 }).ToList();
        stopwatch.Stop();

        _logger.Verbose("Registered {HashCount} distinct tile(s) and {AnimationCount} tile animation(s) for {FileName}. Took: {Elapsed}ms",
            registeredTiles.Count, animationTiles.Count, fileName, stopwatch.ElapsedMilliseconds);

        // Generate the final tileset image containing all unique tiles
        var tilesetImage = _tilesetImageFactory.CreateFromTiles(registeredTiles, fileName);
        var tileset = new Tileset
        {
            Name = fileName,
            TileWidth = _tileSize.Width,
            TileHeight = _tileSize.Height,
            Margin = _tileMargin,
            Spacing = _tileSpacing,
            TileCount = registeredTiles.Count,
            Columns = tilesetImage.Width / _tileSize.Width,
            Image = tilesetImage,
            AnimatedTiles = animationTiles,
            RegisteredTiles = registeredTiles,
            HashAccumulations = hashAccumulations,
            OriginalSize = frames[0].Size
        };

        return tileset;
    }

    private void ExtractAndHashTiles(List<Image<Rgba32>> frames,
        Dictionary<Point, List<TilesetTileImage>> tileImages,
        Dictionary<Point, uint> hashAccumulations)
    {
        foreach (var frame in frames)
        {
            foreach (var tileLocation in GetTileLocations(frame.Width, frame.Height))
            {
                // Extract a single tile from the current frame at this location
                var tileBounds = new Rectangle(tileLocation, _tileSize);
                var tileFrame = frame.Clone();
                tileFrame.Mutate(ctx => ctx.Crop(tileBounds));

                var tileHash = _imageHashService.Compute(tileFrame);
                var tileImage = new TilesetTileImage(tileFrame, tileHash);

                // Accumulate hash values for this tile position to create a unique identifier
                // for the entire animation sequence at this position
                if (hashAccumulations.TryGetValue(tileLocation, out var accumulation))
                {
                    // FNV-1a hash combining algorithm to generate a unique hash for the animation sequence
                    hashAccumulations[tileLocation] = accumulation * 33 ^ tileHash;
                }
                else
                {
                    hashAccumulations.Add(tileLocation, tileHash);
                }

                if (tileImages.TryGetValue(tileLocation, out var locationImages) == false)
                {
                    locationImages = [];
                    tileImages.Add(tileLocation, locationImages);
                }

                locationImages.Add(tileImage);
            }
        }
    }

    private void CreateTileAnimations(Dictionary<Point, List<TilesetTileImage>> tileImages,
        Dictionary<Point, uint> hashAccumulations,
        List<TilesetTile> registeredTiles,
        Dictionary<int, TilesetTile> imageTileMap,
        int animationDuration)
    {
        // Process each unique tile position (identified by its combined hash value)
        foreach (var (tileLocation, hashAccumulation) in hashAccumulations.DistinctBy(h => h.Value))
        {
            var tileImageCollection = tileImages[tileLocation];
            var tile = new TilesetTile
            {
                Id = registeredTiles.Count,
                Image = tileImageCollection[0],
                Animation = new TilesetTileAnimation
                {
                    Frames = [],
                    Hash = hashAccumulation
                }
            };
            registeredTiles.Add(tile);
            imageTileMap[tile.Image.GetHashCode()] = tile;

            // Optimize animation by combining consecutive identical frames
            var previousTileImage = tileImageCollection[0];
            var previousFrameDuration = 0;
            foreach (var currentTileImage in tileImageCollection)
            {
                if (currentTileImage.Equals(previousTileImage))
                {
                    // If current frame is identical to previous, extend the duration rather than add new frame
                    previousFrameDuration += _frameDuration;
                }
                else
                {
                    // When a change is detected, add the previous frame with its accumulated duration
                    AddAnimationFrame(tile, registeredTiles, previousTileImage, previousFrameDuration, imageTileMap);
                    previousTileImage = currentTileImage;
                    previousFrameDuration = _frameDuration;
                }
            }

            // Add the final frame with remaining duration
            // Calculate remaining time by subtracting all frames already accounted for from total animation time
            AddAnimationFrame(tile, registeredTiles, previousTileImage, animationDuration - tile.Animation.Frames.Sum(f => f.Duration), imageTileMap);
        }
    }

    private static void AddAnimationFrame(TilesetTile tile,
        List<TilesetTile> registeredTiles, TilesetTileImage tileImage, int duration, Dictionary<int, TilesetTile> imageTileMap)
    {
        // Reuse existing tile if this exact image has been seen before, otherwise register a new one
        TilesetTile registeredTile;
        if (imageTileMap.TryGetValue(tileImage.GetHashCode(), out var existingTile))
        {
            registeredTile = existingTile;
        }
        else
        {
            registeredTile = new TilesetTile
            {
                Id = registeredTiles.Count,
                Image = tileImage
            };
            registeredTiles.Add(registeredTile);
            imageTileMap[tileImage.GetHashCode()] = registeredTile;
        }

        // Create animation frame referencing the tile and add to animation
        var tileAnimationFrame = new TilesetTileAnimationFrame
        {
            TileId = registeredTile.Id,
            Duration = duration
        };
        tile.Animation!.Frames.Add(tileAnimationFrame);
    }

    private IEnumerable<Point> GetTileLocations(int width, int height)
    {
        // Generate grid coordinates for each tile position in the image
        for (var y = 0; y < height; y += _tileSize.Height)
        {
            for (var x = 0; x < width; x += _tileSize.Width)
            {
                yield return new Point(x, y);
            }
        }
    }
}
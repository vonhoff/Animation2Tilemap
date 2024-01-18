﻿using System.Diagnostics;
using Animation2Tilemap.Core.Entities;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Services.Contracts;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Animation2Tilemap.Core.Factories;

public class TilesetFactory(ApplicationOptions options, ITilesetImageFactory tilesetImageFactory, IImageHashService imageHashService, ILogger logger)
    : ITilesetFactory
{
    private readonly int _frameDuration = options.FrameDuration;
    private readonly int _tileMargin = options.TileMargin;
    private readonly Size _tileSize = options.TileSize;
    private readonly int _tileSpacing = options.TileSpacing;

    public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
    {
        var tileImages = new Dictionary<Point, List<TilesetTileImage>>();
        var hashAccumulations = new Dictionary<Point, uint>();
        var registeredTiles = new List<TilesetTile>();
        var animationDuration = _frameDuration * frames.Count;
        var stopwatch = Stopwatch.StartNew();

        foreach (var frame in frames)
        {
            foreach (var tileLocation in GetTileLocations(frame.Width, frame.Height))
            {
                var tileBounds = new Rectangle(tileLocation, _tileSize);
                var tileFrame = frame.Clone();
                tileFrame.Mutate(ctx => ctx.Crop(tileBounds));

                var tileHash = imageHashService.Compute(tileFrame);
                var tileImage = new TilesetTileImage(tileFrame, tileHash);

                if (hashAccumulations.TryGetValue(tileLocation, out var accumulation))
                {
                    hashAccumulations[tileLocation] = accumulation * 33 ^ tileHash;
                }
                else
                {
                    hashAccumulations.Add(tileLocation, tileHash);
                }

                if (tileImages.TryGetValue(tileLocation, out var locationImages) == false)
                {
                    locationImages = new List<TilesetTileImage>();
                    tileImages.Add(tileLocation, locationImages);
                }

                locationImages.Add(tileImage);
            }
        }

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

        var animationTiles = registeredTiles.Where(t => t.Animation is { Frames.Count: > 1 }).ToList();
        stopwatch.Stop();

        logger.Verbose("Registered {HashCount} distinct tile(s) and {AnimationCount} tile animation(s) for {FileName}. Took: {Elapsed}ms",
            registeredTiles.Count, animationTiles.Count, fileName, stopwatch.ElapsedMilliseconds);

        var tilesetImage = tilesetImageFactory.CreateFromTiles(registeredTiles, fileName);
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

    private static void AddAnimationFrame(TilesetTile tile,
        List<TilesetTile> registeredTiles, TilesetTileImage tileImage, int duration)
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
        tile.Animation!.Frames.Add(tileAnimationFrame);
    }

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
}
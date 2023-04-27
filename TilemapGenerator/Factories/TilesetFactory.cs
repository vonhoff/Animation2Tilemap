using TilemapGenerator.Common.CommandLine;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories
{
    public class TilesetFactory : ITilesetFactory
    {
        private readonly Size _tileSize;
        private readonly int _frameDuration;
        private readonly ITilesetImageFactory _tilesetImageFactory;
        private readonly ITileHashService _tileHashService;
        private readonly IHashCodeCombinerService _hashCodeCombinerService;

        public TilesetFactory(
            CommandLineOptions options,
            ITilesetImageFactory tilesetImageFactory,
            ITileHashService tileHashService,
            IHashCodeCombinerService hashCodeCombinerService)
        {
            _tileHashService = tileHashService;
            _hashCodeCombinerService = hashCodeCombinerService;
            _tilesetImageFactory = tilesetImageFactory;
            _tileSize = options.TileSize;
            _frameDuration = options.AnimationFrameDuration;
        }

        public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
        {
            var tileCollections = new Dictionary<Point, List<TilesetTileImage>>();
            var hashAccumulations = new Dictionary<Point, int>();
            var tileRegister = new List<TilesetTile>();
            
            foreach (var frame in frames)
            {
                ComputeTileHashAccumulations(frame, hashAccumulations, tileCollections);
            }
            
            var animationDuration = _frameDuration * frames.Count;
            foreach (var hashAccumulation in hashAccumulations.DistinctBy(a => a.Value))
            {
                CreateTilesFromCollection(tileCollections, hashAccumulation, tileRegister, animationDuration);
            }

            var tilesetImage = _tilesetImageFactory.CreateFromTiles(tileRegister, fileName);
            var tileset = new Tileset
            {
                Name = fileName,
                TileWidth = _tileSize.Width,
                TileHeight = _tileSize.Height,
                TileCount = tileRegister.Count,
                Image = tilesetImage,
                AnimatedTiles = tileRegister.Where(t => t.Animation is { Frames.Count: > 1 }).ToList(),
                RegisteredTiles = tileRegister
            };

            return tileset;
        }

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
                var remainingTime = animationDuration - (i * _frameDuration);

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
                    var tileHash = _tileHashService.Compute(tileFrame);
                    var tileImage = new TilesetTileImage(tileFrame, tileHash);

                    // Accumulate the tile hash collection at this location.
                    if (hashAccumulations.TryGetValue(tileLocation, out var accumulation))
                    {
                        hashAccumulations[tileLocation] = _hashCodeCombinerService.CombineHashCodes(accumulation, tileHash);
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
}
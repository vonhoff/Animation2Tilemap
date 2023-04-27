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
        private readonly ITileHashService _tileHashService;
        private readonly IHashCodeCombinerService _hashCodeCombinerService;

        public TilesetFactory(
            CommandLineOptions options,
            ITileHashService tileHashService,
            IHashCodeCombinerService hashCodeCombinerService)
        {
            _tileHashService = tileHashService;
            _tileSize = options.TileSize;
            _frameDuration = options.AnimationFrameDuration;
            _hashCodeCombinerService = hashCodeCombinerService;
        }

        public Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames)
        {
            // This keeps track of all tiles that appeared at a certain location. 
            var tileCollections = new Dictionary<Point, List<TilesetTileImage>>();

            // This keeps track of the animation hash, where the key represents the location,
            // and the value is the accumulated hash of all tiles that have appeared at that location.
            var hashAccumulations = new Dictionary<Point, int>();

            // This value is used as the final tile collection for the tileset.
            var tileRegister = new List<TilesetTile>();

            // Loop through every frame and compute the tile hashes and animation hashes.
            foreach (var frame in frames)
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

            // Determine the overall animation duration based on the number of frames and frame duration.
            var animationDuration = _frameDuration * frames.Count;

            // Iterate through the accumulated hashes and create a new TilesetTile object for every accumulation.
            foreach (var hashAccumulation in hashAccumulations.DistinctBy(a => a.Value))
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
                // We can skip the first tile from the collection since that tile is always added.
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

            var tileset = new Tileset
            {
                AnimatedTiles = tileRegister
            };

            return tileset;
        }
    }
}
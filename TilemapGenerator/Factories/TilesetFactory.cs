using TilemapGenerator.Common.CommandLine;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

// Kladblok
//var isLastFrame = index == frames.Count - 1;
//var remainingTime = animationDuration - _frameDuration * index;

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
            var tileId = 0;
            
            // This keeps track of the amount of unique tiles within all frames, where the key represents the image,
            // and the value is the tileId for assigning them later.
            var registeredTiles = new Dictionary<TilesetTileImage, int>();

            // This keeps track of all tiles that appeared at a certain location. 
            var tileCollections = new Dictionary<Point, List<TilesetTileImage>>();

            // This keeps track of the animation hash, where the key represents the location,
            // and the value is the accumulated hash of all tiles that have appeared at that location.
            var tileAccumulations = new Dictionary<Point, int>();

            // Loop through every frame and compute the tile hashes and animation hashes.
            // Keep track of unique tiles and their ID's.
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

                        // Register tile if it's unique.
                        if (!registeredTiles.ContainsKey(tileImage))
                        {
                            registeredTiles.Add(tileImage, tileId);
                            tileId++;
                        }

                        // Accumulate the tile hash collection at this location.
                        if (tileAccumulations.TryGetValue(tileLocation, out var accumulation))
                        {
                            tileAccumulations[tileLocation] = _hashCodeCombinerService.CombineHashCodes(accumulation, tileHash);
                        }
                        else
                        {
                            tileAccumulations.Add(tileLocation, tileHash);
                        }

                        // Update the collection at this location.
                        if (tileCollections.TryGetValue(tileLocation, out var collection))
                        {
                            collection.Add(tileImage);
                        }
                        else
                        {
                            tileCollections.Add(tileLocation, new List<TilesetTileImage> { tileImage });
                        }
                    }
                }
            }

            var animationDuration = _frameDuration * frames.Count;

            // Iterate through the accumulated hashes and create a new TilesetTile object for every unique tile.

        }
    }
}
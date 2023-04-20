using Serilog;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories
{
    public class TilesetTileFactory : ITilesetTileFactory
    {
        private readonly ITileHashService _hashService;
        private readonly ILogger _logger;

        public TilesetTileFactory(ITileHashService hashService, ILogger logger)
        {
            _hashService = hashService;
            _logger = logger;
        }

        public List<TilesetTile> FromFrames(List<Image<Rgba32>> frames, Size tileSize)
        {
            var tilesetTiles = new List<TilesetTile>();

            foreach (var frame in frames)
            {
                for (var x = 0; x < frame.Width; x += tileSize.Width)
                {
                    for (var y = 0; y < frame.Height; y += tileSize.Height)
                    {
                        var tileHash = _hashService.Compute(frame, tileSize, x, y);
                        var existingTile = tilesetTiles.Find(tilesetTileRecord => tilesetTileRecord.Hash == tileHash);
                        if (existingTile == null)
                        {
                            var tileRect = new Rectangle(x, y, tileSize.Width, tileSize.Height);
                            var tileImage = frame.Clone(ctx => ctx.Crop(tileRect));
                            var tile = new TilesetTile
                            {
                                Id = tilesetTiles.Count + 1,
                                Location = new Point(x, y),
                                Hash = tileHash,
                                Image = tileImage
                            };

                            tilesetTiles.Add(tile);
                        }
                    }
                }
            }

            return tilesetTiles;
        }
    }
}
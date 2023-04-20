using System;
using Serilog;
using TilemapGenerator.Records;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class TileRecordService : ITileRecordService
    {
        private readonly ITileHashService _hashService;
        private readonly ILogger _logger;

        public TileRecordService(ITileHashService hashService, ILogger logger)
        {
            _hashService = hashService;
            _logger = logger;
        }

        public List<TileRecord> FromFrames(List<Image<Rgba32>> frames, Size tileSize)
        {
            var tileRecords = new List<TileRecord>();
          
            foreach (var frame in frames)
            {
                for (var x = 0; x < frame.Width; x += tileSize.Width)
                {
                    for (var y = 0; y < frame.Height; y += tileSize.Height)
                    {
                        var tileHash = 17;

                        for (var tileX = x; tileX < x + tileSize.Width; tileX++)
                        {
                            for (var tileY = y; tileY < y + tileSize.Height; tileY++)
                            {
                                var pixelColor = frame[tileX, tileY];
                                tileHash = _hashService.Compute(pixelColor, tileHash, tileX, tileY);
                            }
                        }

                        var tile = tileRecords.Find(tilesetTileRecord => tilesetTileRecord.Hash == tileHash);
                        if (tile == null)
                        {
                            var tileRect = new Rectangle(x, y, tileSize.Width, tileSize.Height);
                            var tileImage = frame.Clone(ctx => ctx.Crop(tileRect));
                            tileRecords.Add(new TileRecord(tileRecords.Count + 1, new Point(x, y), tileHash, tileImage));
                        }
                    }
                }
            }

            return tileRecords;
        }
    }
}
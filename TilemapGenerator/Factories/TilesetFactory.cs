using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Records;

namespace TilemapGenerator.Factories
{
    public class TilesetFactory : ITilesetFactory
    {
        public Tileset FromTileRecords(List<TileRecord> tileRecords, Size tileSize)
        {
            var groupedRecords = tileRecords
                .GroupBy(tileRecord => tileRecord.Location)
                .Where(grouping => grouping.Count() > 1);

            var tiles = new List<TilesetTile>();

            foreach (var group in groupedRecords)
            {
                var animation = new TilesetTileAnimation();

                foreach (var record in group)
                {
                    var frame = new TilesetTileAnimationFrame
                    {
                        TileId = record.Id,
                        Duration = 0 // TODO: Set this to the actual duration
                    };

                    animation.Frames.Add(frame);
                }

                var tile = new TilesetTile
                {
                    Id = group.First().Id,
                    Animation = animation
                };

                tiles.Add(tile);
            }

            var tileset = new Tileset
            {
                TileWidth = tileSize.Width,
                TileHeight = tileSize.Height,
                Columns = 0, // TODO: Calculate this based on the tileset image size
                TileCount = tiles.Count,
                Image = null, // TODO: Set this to the tileset image data if available
                Name = "", // TODO: Set this to a meaningful name if available
                Tiles = tiles
            };

            return tileset;
        }
    }
}
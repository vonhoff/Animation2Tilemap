using TilemapGenerator.Entities;
using TilemapGenerator.Records;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        Tileset FromTileRecords(List<TileRecord> tileRecords, Size tileSize);
    }
}
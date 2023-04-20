using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        Tileset FromTiles(List<TilesetTile> tileRecords, Size tileSize);
    }
}
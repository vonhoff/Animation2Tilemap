using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts;

public interface ITilesetImageFactory
{
    TilesetImage CreateFromTiles(List<TilesetTile> tileRegister, string fileName);
}
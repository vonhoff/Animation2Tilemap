using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts;

public interface ITilesetImageFactory
{
    TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName);
}
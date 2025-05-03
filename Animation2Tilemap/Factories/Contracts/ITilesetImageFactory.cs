using Animation2Tilemap.Entities;

namespace Animation2Tilemap.Factories.Contracts;

public interface ITilesetImageFactory
{
    TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName);
}
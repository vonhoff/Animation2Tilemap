using Animation2Tilemap.Core.Entities;

namespace Animation2Tilemap.Core.Factories.Contracts;

public interface ITilesetImageFactory
{
    TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName);
}
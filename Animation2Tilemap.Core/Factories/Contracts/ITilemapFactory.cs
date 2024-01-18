using Animation2Tilemap.Core.Entities;

namespace Animation2Tilemap.Core.Factories.Contracts;

public interface ITilemapFactory
{
    Tilemap CreateFromTileset(Tileset tileset);
}
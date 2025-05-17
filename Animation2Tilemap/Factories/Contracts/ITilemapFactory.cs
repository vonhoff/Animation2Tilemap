using Animation2Tilemap.Entities;

namespace Animation2Tilemap.Factories.Contracts;

public interface ITilemapFactory
{
    Tilemap CreateFromTileset(Tileset tileset, List<int> frameTimes);
}
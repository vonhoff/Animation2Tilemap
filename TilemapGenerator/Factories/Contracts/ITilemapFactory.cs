using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts;

public interface ITilemapFactory
{
    Tilemap CreateFromTileset(Tileset tileset);
}
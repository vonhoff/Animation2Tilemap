using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;

namespace TilemapGenerator.Factories;

public class TilemapFactory : ITilemapFactory
{
    public Tilemap CreateFromTileset(Tileset tileset)
    {
        return new Tilemap();
    }
}
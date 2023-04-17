using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilemapFactory
    {
        Tilemap FromTileset(Tileset tileset, List<Image<Rgba32>> frames);
    }
}
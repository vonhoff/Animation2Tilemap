using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        List<Tileset> FromImageCollection(Dictionary<string, List<Image<Rgba32>>> images);
    }
}
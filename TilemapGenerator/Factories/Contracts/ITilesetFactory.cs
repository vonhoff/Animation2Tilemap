using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        List<Tileset> FromCollection(Dictionary<string, List<Image<Rgba32>>> images);
    }
}
using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames);
    }
}
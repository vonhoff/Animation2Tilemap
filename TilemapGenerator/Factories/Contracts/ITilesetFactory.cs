using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetFactory
    {
        Tileset FromFrames(List<Image<Rgba32>> frames, Size tileSize);
        Tileset FromFramesConsolidated(List<Image<Rgba32>> frames, Size tileSize);
    }
}
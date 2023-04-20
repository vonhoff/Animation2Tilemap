using TilemapGenerator.Entities;

namespace TilemapGenerator.Factories.Contracts
{
    public interface ITilesetTileFactory
    {
        List<TilesetTile> FromFrames(List<Image<Rgba32>> frames, Size tileSize);
    }
}
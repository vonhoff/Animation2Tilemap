using TilemapGenerator.Entities;

namespace TilemapGenerator.Utilities
{
    public static class TilesetUtility
    {
        public static Tileset FromFrames(string filename, IReadOnlyList<Image<Rgba32>> frames)
        {
            return new Tileset();
        }
    }
}
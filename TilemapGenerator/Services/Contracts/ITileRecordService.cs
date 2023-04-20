using TilemapGenerator.Records;

namespace TilemapGenerator.Services.Contracts
{
    public interface ITileRecordService
    {
        List<TileRecord> FromFrames(List<Image<Rgba32>> frames, Size tileSize);
    }
}
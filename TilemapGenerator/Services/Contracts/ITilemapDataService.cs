using System.IO.Compression;
using TilemapGenerator.Enums;

namespace TilemapGenerator.Services.Contracts
{
    public interface ITilemapDataService
    {
        List<uint> ParseDataAsBase64(string input, TilemapDataCompression compression);

        string SerializeDataAsBase64(TilemapDataCompression compression, List<uint> data);
    }
}
using System.IO.Compression;
using TilemapGenerator.Enums;

namespace TilemapGenerator.Services.Contracts
{
    public interface ITilemapDataService
    {
        List<uint> ParseData(string input, TilemapDataFormat format);

        string SerializeData(List<uint> data, TilemapDataFormat format);
    }
}
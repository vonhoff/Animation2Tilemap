using System.IO.Compression;

namespace TilemapGenerator.Services.Contracts
{
    public interface ITilemapDataService
    {
        void ParseDataAsBase64(string input, string? compression, ref int[] data, ref byte[] dataRotationFlags);

        string SerializeDataAsBase64(int[] data, byte[] dataRotationFlags, string? compression);
    }
}
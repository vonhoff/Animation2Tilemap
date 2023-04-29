using System.IO.Compression;
using TilemapGenerator.Enums;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class TilemapDataService : ITilemapDataService
    {
        public List<uint> ParseDataAsBase64(string input, TilemapDataCompression compression)
        {
            var decoded = Convert.FromBase64String(input);
            var inputStream = new MemoryStream(decoded);

            using Stream compressorStream = compression switch
            {
                TilemapDataCompression.None => inputStream,
                TilemapDataCompression.ZLib => new ZLibStream(inputStream, CompressionMode.Decompress, false),
                TilemapDataCompression.GZip => new GZipStream(inputStream, CompressionMode.Decompress, false),
                _ => throw new ArgumentOutOfRangeException(nameof(compression), compression, null)
            };

            var data = new List<uint>();
            Span<byte> decompressedDataBuffer = stackalloc byte[4];
            while (compressorStream.Read(decompressedDataBuffer) == 4)
            {
                data.Add(BitConverter.ToUInt32(decompressedDataBuffer));
            }

            compressorStream.Close();
            return data;
        }

        public string SerializeDataAsBase64(TilemapDataCompression compression, List<uint> data)
        {
            var dataBytes = new byte[data.Count * 4];
            for (var i = 0; i < data.Count; i++)
            {
                var bytes = BitConverter.GetBytes(data[i]);
                Array.Copy(bytes, 0, dataBytes, i * 4, 4);
            }

            var outputStream = new MemoryStream();
            Stream compressorStream = compression switch
            {
                TilemapDataCompression.None => outputStream,
                TilemapDataCompression.ZLib => new ZLibStream(outputStream, CompressionMode.Compress, false),
                TilemapDataCompression.GZip => new GZipStream(outputStream, CompressionMode.Compress, false),
                _ => throw new ArgumentOutOfRangeException(nameof(compression), compression, null)
            };
            compressorStream.Write(dataBytes);
            compressorStream.Close();

            var encoded = Convert.ToBase64String(outputStream.ToArray());
            return encoded;
        }
    }
}
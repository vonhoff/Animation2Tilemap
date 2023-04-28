using System.IO.Compression;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class TilemapDataService : ITilemapDataService
    {
        private const uint FLIPPED_HORIZONTALLY_FLAG = 0b10000000000000000000000000000000;
        private const uint FLIPPED_VERTICALLY_FLAG = 0b01000000000000000000000000000000;
        private const uint FLIPPED_DIAGONALLY_FLAG = 0b00100000000000000000000000000000;
        private const int SHIFT_FLIP_FLAG_TO_BYTE = 29;

        public void ParseDataAsBase64(string input, string? compression, ref int[] data, ref byte[] dataRotationFlags)
        {
            using (var base64DataStream = new MemoryStream(Convert.FromBase64String(input)))
            {
                if (compression == null)
                {
                    // Parse the decoded bytes and update the inner data as well as the data rotation flags
                    var rawBytes = new byte[4];
                    data = new int[base64DataStream.Length];
                    dataRotationFlags = new byte[base64DataStream.Length];

                    for (var i = 0; i < base64DataStream.Length; i++)
                    {
                        base64DataStream.Read(rawBytes, 0, rawBytes.Length);
                        var rawID = BitConverter.ToUInt32(rawBytes, 0);
                        var hor = ((rawID & FLIPPED_HORIZONTALLY_FLAG));
                        var ver = ((rawID & FLIPPED_VERTICALLY_FLAG));
                        var dia = ((rawID & FLIPPED_DIAGONALLY_FLAG));
                        dataRotationFlags[i] = (byte)((hor | ver | dia) >> SHIFT_FLIP_FLAG_TO_BYTE);

                        // assign data to rawID with the rotation flags cleared
                        data[i] = (int)(rawID & ~(FLIPPED_HORIZONTALLY_FLAG | FLIPPED_VERTICALLY_FLAG | FLIPPED_DIAGONALLY_FLAG));
                    }
                }
                else if (compression == "zlib")
                {
                    // .NET doesn't play well with the headered zlib data that Tiled produces,
                    // so we have to manually skip the 2-byte header to get what DeflateStream's looking for
                    // Should an external library be used instead of this hack?
                    base64DataStream.ReadByte();
                    base64DataStream.ReadByte();

                    using (var decompressionStream = new DeflateStream(base64DataStream, CompressionMode.Decompress))
                    {
                        // Parse the raw decompressed bytes and update the inner data as well as the data rotation flags
                        var decompressedDataBuffer = new byte[4]; // size of each tile
                        var dataRotationFlagsList = new List<byte>();
                        var layerDataList = new List<int>();

                        while (decompressionStream.Read(decompressedDataBuffer, 0, decompressedDataBuffer.Length) == decompressedDataBuffer.Length)
                        {
                            var rawID = BitConverter.ToUInt32(decompressedDataBuffer, 0);
                            var hor = ((rawID & FLIPPED_HORIZONTALLY_FLAG));
                            var ver = ((rawID & FLIPPED_VERTICALLY_FLAG));
                            var dia = ((rawID & FLIPPED_DIAGONALLY_FLAG));
                            dataRotationFlagsList.Add((byte)((hor | ver | dia) >> SHIFT_FLIP_FLAG_TO_BYTE));

                            // assign data to rawID with the rotation flags cleared
                            layerDataList.Add((int)(rawID & ~(FLIPPED_HORIZONTALLY_FLAG | FLIPPED_VERTICALLY_FLAG | FLIPPED_DIAGONALLY_FLAG)));
                        }

                        data = layerDataList.ToArray();
                        dataRotationFlags = dataRotationFlagsList.ToArray();
                    }
                }
                else if (compression == "gzip")
                {
                    using (var decompressionStream = new GZipStream(base64DataStream, CompressionMode.Decompress))
                    {
                        // Parse the raw decompressed bytes and update the inner data as well as the data rotation flags
                        var decompressedDataBuffer = new byte[4]; // size of each tile
                        var dataRotationFlagsList = new List<byte>();
                        var layerDataList = new List<int>();

                        while (decompressionStream.Read(decompressedDataBuffer, 0, decompressedDataBuffer.Length) == decompressedDataBuffer.Length)
                        {
                            var rawID = BitConverter.ToUInt32(decompressedDataBuffer, 0);
                            var hor = ((rawID & FLIPPED_HORIZONTALLY_FLAG));
                            var ver = ((rawID & FLIPPED_VERTICALLY_FLAG));
                            var dia = ((rawID & FLIPPED_DIAGONALLY_FLAG));

                            dataRotationFlagsList.Add((byte)((hor | ver | dia) >> SHIFT_FLIP_FLAG_TO_BYTE));

                            // assign data to rawID with the rotation flags cleared
                            layerDataList.Add((int)(rawID & ~(FLIPPED_HORIZONTALLY_FLAG | FLIPPED_VERTICALLY_FLAG | FLIPPED_DIAGONALLY_FLAG)));
                        }

                        data = layerDataList.ToArray();
                        dataRotationFlags = dataRotationFlagsList.ToArray();
                    }
                }
                else
                {
                    throw new Exception("Zstandard compression is currently not supported");
                }
            }
        }

        public string SerializeDataAsBase64(int[] data, byte[] dataRotationFlags, string? compression)
        {
            return string.Empty;
        }
    }
}
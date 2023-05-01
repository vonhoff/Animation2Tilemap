using System.IO.Compression;
using Animation2Tilemap.Enums;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Services;

public sealed class TilemapDataService : ITilemapDataService
{
    /// <summary>
    /// Parses a string of tile data in the specified format and returns a list of tile IDs as uints.
    /// </summary>
    /// <param name="input">The tile data string to be parsed.</param>
    /// <param name="format">The format of the tile data string.</param>
    /// <returns>A list of tile IDs as uints.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the provided format is unsupported.</exception>
    public List<uint> ParseData(string input, TileLayerFormat format)
    {
        byte[] data;
        switch (format)
        {
            case TileLayerFormat.Base64Uncompressed or TileLayerFormat.Base64ZLib or TileLayerFormat.Base64GZip:
            {
                data = Convert.FromBase64String(input);
                break;
            }
            case TileLayerFormat.CSV:
            {
                var csvTileIds = input.Trim().Split(',');
                data = new byte[csvTileIds.Length * 4];
                for (var i = 0; i < csvTileIds.Length; i++)
                {
                    var tileId = uint.Parse(csvTileIds[i].Trim());
                    var tileIdBytes = BitConverter.GetBytes(tileId);
                    Array.Copy(tileIdBytes, 0, data, i * 4, 4);
                }

                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException(nameof(format), format, "Unsupported format");
            }
        }

        using var inputStream = new MemoryStream(data);
        using Stream compressorStream = format switch
        {
            TileLayerFormat.CSV or TileLayerFormat.Base64Uncompressed => inputStream,
            TileLayerFormat.Base64ZLib => new ZLibStream(inputStream, CompressionMode.Decompress, false),
            TileLayerFormat.Base64GZip => new GZipStream(inputStream, CompressionMode.Decompress, false),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };

        var result = new List<uint>();
        Span<byte> decompressedDataBuffer = stackalloc byte[4];
        while (compressorStream.Read(decompressedDataBuffer) == 4)
        {
            result.Add(BitConverter.ToUInt32(decompressedDataBuffer));
        }

        compressorStream.Close();
        return result;
    }

    /// <summary>
    /// Serializes a list of 32-bit unsigned integers into a string representation based on the specified format.
    /// </summary>
    /// <param name="data">The list of 32-bit unsigned integers to be serialized.</param>
    /// <param name="format">The format to be used for serialization.</param>
    /// <returns>The serialized string representation of the list of 32-bit unsigned integers.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the specified format is not supported.</exception>
    public string SerializeData(List<uint> data, TileLayerFormat format)
    {
        var dataBytes = new byte[data.Count * 4];
        for (var i = 0; i < data.Count; i++)
        {
            var bytes = BitConverter.GetBytes(data[i]);
            Array.Copy(bytes, 0, dataBytes, i * 4, 4);
        }

        using var outputStream = new MemoryStream();
        Stream compressorStream = format switch
        {
            TileLayerFormat.CSV or TileLayerFormat.Base64Uncompressed => outputStream,
            TileLayerFormat.Base64ZLib => new ZLibStream(outputStream, CompressionMode.Compress, false),
            TileLayerFormat.Base64GZip => new GZipStream(outputStream, CompressionMode.Compress, false),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
        compressorStream.Write(dataBytes);
        compressorStream.Close();

        var encoded = format switch
        {
            TileLayerFormat.Base64Uncompressed or TileLayerFormat.Base64ZLib or TileLayerFormat.Base64GZip => Convert.ToBase64String(outputStream.ToArray()),
            TileLayerFormat.CSV => string.Join(",", data.Select(d => d.ToString())),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
        return encoded;
    }
}
using TilemapGenerator.Enums;

namespace TilemapGenerator;

public class ApplicationOptions
{
    public ApplicationOptions(
        int frameDuration,
        string input,
        string output,
        int tileHeight,
        int tileWidth,
        string transparentColor,
        string tileLayerFormat,
        bool verbose)
    {
        FrameDuration = frameDuration;
        Input = input;
        Output = output;
        TileSize = new Size(tileWidth, tileHeight);
        TransparentColor = Rgba32.ParseHex(transparentColor);
        Verbose = verbose;

        TileLayerFormat = tileLayerFormat switch
        {
            "base64" => TileLayerFormat.Base64Uncompressed,
            "zlib" => TileLayerFormat.Base64ZLib,
            "gzip" => TileLayerFormat.Base64GZip,
            "csv" => TileLayerFormat.CSV,
            _ => throw new ArgumentException("Invalid tile layer format.", nameof(tileLayerFormat))
        };
    }

    public int FrameDuration { get; }
    public string Input { get; }
    public string Output { get; }
    public Size TileSize { get; }
    public Rgba32 TransparentColor { get; }
    public TileLayerFormat TileLayerFormat { get; }
    public bool Verbose { get; }
}
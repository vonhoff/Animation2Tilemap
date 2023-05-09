using Animation2Tilemap.Enums;

namespace Animation2Tilemap;

public class ApplicationOptions
{
    public ApplicationOptions(
        int frameDuration,
        string input,
        string output,
        Size tileSize,
        int tileMargin,
        int tileSpacing,
        Rgba32 transparentColor,
        TileLayerFormat tileLayerFormat,
        bool verbose)
    {
        FrameDuration = frameDuration;
        Input = input;
        Output = output;
        TileMargin = tileMargin;
        TileSpacing = tileSpacing;
        TileSize = tileSize;
        TransparentColor = transparentColor;
        TileLayerFormat = tileLayerFormat;
        Verbose = verbose;
    }

    public int FrameDuration { get; }
    public string Input { get; }
    public string Output { get; }
    public Size TileSize { get; }
    public int TileMargin { get; }
    public int TileSpacing { get; }
    public Rgba32 TransparentColor { get; }
    public TileLayerFormat TileLayerFormat { get; }
    public bool Verbose { get; }
}
using Animation2Tilemap.Enums;

namespace Animation2Tilemap;

public class ApplicationOptions
{
    public int FrameDuration { get; init; }
    public string Input { get; init; } = null!;
    public string Output { get; init; } = null!;
    public Size TileSize { get; init; }
    public int TileMargin { get; init; }
    public int TileSpacing { get; init; }
    public Rgba32 TransparentColor { get; init; }
    public TileLayerFormat TileLayerFormat { get; init; }
    public bool Verbose { get; init; }
}
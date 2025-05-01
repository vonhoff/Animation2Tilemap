﻿using Animation2Tilemap.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Workflows;

public class MainWorkflowOptions
{
    public int FrameDuration { get; set; }
    public string Input { get; set; } = null!;
    public string Output { get; set; } = null!;
    public Size TileSize { get; set; }
    public int TileMargin { get; set; }
    public int TileSpacing { get; set; }
    public Rgba32 TransparentColor { get; set; }
    public TileLayerFormat TileLayerFormat { get; set; }
    public bool Verbose { get; set; }
    public bool AssumeAnimation { get; set; }
    public IProgress<(double, string)>? Progress { get; set; }
    public CancellationToken CancellationToken { get; set; } = default;
}
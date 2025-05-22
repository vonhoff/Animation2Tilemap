using System.Xml.Serialization;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Entities;

public class TilesetImage
{
    [XmlAttribute("source")] public string? Path { get; set; }

    [XmlAttribute("trans")] public string? Trans { get; set; }

    [XmlAttribute("width")] public int Width { get; set; }

    [XmlAttribute("height")] public int Height { get; set; }

    [XmlIgnore] public Image<Rgba32> Data { get; init; } = null!;
}
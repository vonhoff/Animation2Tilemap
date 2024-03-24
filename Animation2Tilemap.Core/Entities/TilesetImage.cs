using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Xml.Serialization;

namespace Animation2Tilemap.Core.Entities;

public class TilesetImage
{
    [XmlAttribute("source")]
    public string? Path { get; set; }

    [XmlAttribute("trans")]
    public string? Trans { get; set; }

    [XmlAttribute("width")]
    public int Width { get; set; }

    [XmlAttribute("height")]
    public int Height { get; set; }

    [XmlIgnore]
    public Image<Rgba32> Data { get; init; } = null!;
}
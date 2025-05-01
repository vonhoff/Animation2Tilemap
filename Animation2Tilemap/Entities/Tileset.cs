using SixLabors.ImageSharp;
using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

[XmlRoot("tileset")]
public class Tileset
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("tilewidth")]
    public int TileWidth { get; set; }

    [XmlAttribute("tileheight")]
    public int TileHeight { get; set; }

    [XmlAttribute("spacing")]
    public int Spacing { get; set; }

    [XmlAttribute("margin")]
    public int Margin { get; set; }

    [XmlAttribute("tilecount")]
    public int TileCount { get; set; }

    [XmlAttribute("columns")]
    public int Columns { get; set; }

    [XmlElement("image")]
    public TilesetImage Image { get; set; } = null!;

    [XmlElement("tile")]
    public List<TilesetTile> AnimatedTiles { get; set; } = null!;

    [XmlIgnore]
    public IReadOnlyList<TilesetTile> RegisteredTiles { get; set; } = null!;

    [XmlIgnore]
    public IReadOnlyDictionary<Point, uint> HashAccumulations { get; set; } = null!;

    [XmlIgnore]
    public Size OriginalSize { get; set; }
}
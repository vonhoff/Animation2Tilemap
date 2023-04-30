using System.Xml.Serialization;

namespace TilemapGenerator.Entities;

[XmlRoot("tileset")]
public sealed class Tileset
{
    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("tilewidth")]
    public int TileWidth { get; set; }

    [XmlAttribute("tileheight")]
    public int TileHeight { get; set; }

    [XmlAttribute("tilecount")]
    public int TileCount { get; set; }

    [XmlAttribute("columns")]
    public int Columns { get; set; }

    [XmlElement("image")]
    public TilesetImage Image { get; set; } = null!;

    [XmlElement("tile")]
    public List<TilesetTile> AnimatedTiles { get; set; } = null!;

    [XmlIgnore]
    public List<TilesetTile> RegisteredTiles { get; set; } = null!;

    [XmlIgnore]
    public Dictionary<Point, int> HashAccumulations { get; set; } = null!;

    [XmlIgnore]
    public Size OriginalSize { get; set; }
}
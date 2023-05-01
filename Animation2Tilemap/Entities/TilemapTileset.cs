using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

[Serializable, XmlRoot("tileset")]
public sealed class TilemapTileset
{
    [XmlAttribute("firstgid")]
    public int FirstGid { get; set; }

    [XmlAttribute("source")]
    public string? Source { get; set; }
}
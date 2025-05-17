using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

public class TilesetProperty
{
    [XmlAttribute("name")]
    public string Name { get; set; } = null!;

    [XmlAttribute("type")]
    public string Type { get; set; } = null!;

    [XmlAttribute("value")]
    public string Value { get; set; } = null!;
} 
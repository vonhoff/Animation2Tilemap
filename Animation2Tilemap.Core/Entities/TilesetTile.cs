using System.Xml.Serialization;

namespace Animation2Tilemap.Core.Entities;

public class TilesetTile
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlElement("animation")]
    public TilesetTileAnimation? Animation { get; set; }

    [XmlIgnore]
    public TilesetTileImage Image { get; init; }
}
using System.Xml.Serialization;

namespace TilemapGenerator.Entities;

public sealed class TilesetTile
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlElement("animation")]
    public TilesetTileAnimation? Animation { get; set; }

    [XmlIgnore]
    public TilesetTileImage Image { get; init; } = null!;
}
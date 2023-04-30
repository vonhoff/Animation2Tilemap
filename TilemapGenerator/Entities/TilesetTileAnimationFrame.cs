using System.Xml.Serialization;

namespace TilemapGenerator.Entities;

public sealed class TilesetTileAnimationFrame
{
    [XmlAttribute("tileid")]
    public int TileId { get; set; }

    [XmlAttribute("duration")]
    public int Duration { get; set; }
}
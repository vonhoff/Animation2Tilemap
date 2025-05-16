using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

public class TilesetTileAnimationFrame
{
    [XmlAttribute("tileid")]
    public int TileId { get; set; }

    [XmlAttribute("duration")]
    public int Duration { get; set; }
}
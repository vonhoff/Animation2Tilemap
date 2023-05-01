using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

public sealed class TilesetTileAnimation
{
    [XmlElement("frame")]
    public List<TilesetTileAnimationFrame> Frames { get; set; } = null!;

    [XmlIgnore]
    public int Hash { get; set; }
}
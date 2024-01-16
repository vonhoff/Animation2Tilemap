using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

public class TilesetTileAnimation
{
    [XmlElement("frame")]
    public List<TilesetTileAnimationFrame> Frames { get; set; } = null!;

    [XmlIgnore]
    public uint Hash { get; init; }
}
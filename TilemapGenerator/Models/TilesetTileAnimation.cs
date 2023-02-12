using System.Xml.Serialization;

namespace TilemapGenerator.Models
{
    public class TilesetTileAnimation
    {
        [XmlElement("frame")]
        public List<TilesetTileAnimationFrame> Frames { get; set; }
    }
}
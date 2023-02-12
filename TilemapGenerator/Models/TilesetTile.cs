using System.Xml.Serialization;

namespace TilemapGenerator.Models
{
    public class TilesetTile
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("animation")]
        public TilesetTileAnimation Animation { get; set; }
    }
}
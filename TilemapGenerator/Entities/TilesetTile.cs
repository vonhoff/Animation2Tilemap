using System.Xml.Serialization;

namespace TilemapGenerator.Entities
{
    public class TilesetTile
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("animation")] 
        public TilesetTileAnimation Animation { get; set; } = new();
    }
}
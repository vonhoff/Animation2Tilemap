using System.Xml.Serialization;

namespace TilemapGenerator.Entities
{
    public class TilesetTile
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("animation")] 
        public TilesetTileAnimation Animation { get; set; } = new();

        [XmlIgnore]
        public Point Location { get; set; }

        [XmlIgnore]
        public int Hash { get; set; }

        [XmlIgnore]
        public Image<Rgba32>? Image { get; set; }
    }
}
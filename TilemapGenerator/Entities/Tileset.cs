using System.Xml.Serialization;

namespace TilemapGenerator.Entities
{
    [XmlRoot("tileset")]
    public class Tileset
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }

        [XmlAttribute("tilecount")]
        public int TileCount { get; set; }

        [XmlAttribute("columns")]
        public int Columns { get; set; }

        [XmlElement("image")]
        public TilesetImage Image { get; set; }

        [XmlElement("tile")]
        public List<TilesetTile> Tiles { get; set; }
    }
}
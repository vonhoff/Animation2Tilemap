using System.Xml.Serialization;

namespace TilemapGenerator.Entities
{
    [Serializable, XmlRoot("tileset")]
    public class TilemapTileset
    {
        [XmlAttribute("firstgid")]
        public int FirstGid { get; set; }

        [XmlAttribute("source")]
        public string Source { get; set; }
    }
}
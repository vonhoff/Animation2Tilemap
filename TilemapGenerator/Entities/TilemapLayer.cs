using System.Xml.Serialization;

namespace TilemapGenerator.Entities
{
    [Serializable, XmlRoot("layer")]
    public class TilemapLayer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlElement("data")]
        public TilemapLayerData Data { get; set; }
    }
}
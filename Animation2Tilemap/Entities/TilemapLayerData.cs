using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

[Serializable, XmlRoot("data")]
public class TilemapLayerData
{
    [XmlAttribute("encoding")]
    public string? Encoding { get; set; }

    [XmlAttribute("compression")]
    public string? Compression { get; set; }

    [XmlText]
    public string? Text { get; set; }
}
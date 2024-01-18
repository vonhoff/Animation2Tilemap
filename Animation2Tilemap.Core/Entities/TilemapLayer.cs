using System.Xml.Serialization;

namespace Animation2Tilemap.Core.Entities;

[Serializable]
[XmlRoot("layer")]
public class TilemapLayer
{
    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("name")]
    public string? Name { get; set; }

    [XmlAttribute("width")]
    public int Width { get; set; }

    [XmlAttribute("height")]
    public int Height { get; set; }

    [XmlElement("data")]
    public TilemapLayerData? Data { get; set; }
}
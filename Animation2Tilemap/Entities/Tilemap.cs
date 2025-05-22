using System.Xml.Serialization;

namespace Animation2Tilemap.Entities;

[Serializable]
[XmlRoot("map")]
public class Tilemap
{
    [XmlAttribute("version")] public string? Version { get; set; }

    [XmlAttribute("orientation")] public string? Orientation { get; set; }

    [XmlAttribute("renderorder")] public string? RenderOrder { get; set; }

    [XmlAttribute("width")] public int Width { get; set; }

    [XmlAttribute("height")] public int Height { get; set; }

    [XmlAttribute("tilewidth")] public int TileWidth { get; set; }

    [XmlAttribute("tileheight")] public int TileHeight { get; set; }

    [XmlAttribute("nextobjectid")] public int NextObjectId { get; set; }

    [XmlElement("tileset")] public TilemapTileset? Tileset { get; set; }

    [XmlElement("layer")] public TilemapLayer? TilemapLayer { get; set; }
}
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TilemapGenerator.Entities;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public class TilesetSerializerService : ITilesetSerializerService
{
    public string Serialize(Tileset tileset)
    {
        var serializer = new XmlSerializer(typeof(Tileset));
        using var memoryStream = new MemoryStream();
        var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            Indent = true
        };

        using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
        {
            serializer.Serialize(xmlWriter, tileset, namespaces);
        }

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}
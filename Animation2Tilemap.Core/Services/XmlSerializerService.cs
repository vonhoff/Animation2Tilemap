using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Animation2Tilemap.Core.Services.Contracts;

namespace Animation2Tilemap.Core.Services;

public class XmlSerializerService : IXmlSerializerService
{
    public string Serialize<T>(T obj) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        var serializer = new XmlSerializer(typeof(T));
        using var memoryStream = new MemoryStream();
        var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
        var settings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false),
            Indent = true
        };

        using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
        {
            serializer.Serialize(xmlWriter, obj, namespaces);
        }

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}
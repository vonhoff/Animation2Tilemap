using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Services;

public sealed class XmlSerializerService : IXmlSerializerService
{
    /// <summary>
    /// Serializes the specified object to an XML string using the XmlSerializer.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>The XML string representation of the serialized object.</returns>
    public string Serialize<T>(T obj) where T : class
    {
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
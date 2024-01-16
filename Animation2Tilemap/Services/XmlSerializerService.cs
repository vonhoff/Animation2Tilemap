﻿using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Services;

public class XmlSerializerService : IXmlSerializerService
{
    public string Serialize<T>(T obj) where T : class
    {
        ArgumentNullException.ThrowIfNull(obj);

        var serializer = new XmlSerializer(typeof(T));
        using var memoryStream = new MemoryStream();
        var namespaces = new XmlSerializerNamespaces([XmlQualifiedName.Empty]);
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
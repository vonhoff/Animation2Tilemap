using Animation2Tilemap.Core.Services;
using System.Xml.Serialization;

namespace Animation2Tilemap.Core.Test.Services;

public class XmlSerializerServiceTests
{
    private readonly Person _person = new()
    {
        Name = "Alice",
        Age = 25,
        Address = new Address
        {
            Street = "Main Street",
            City = "New York",
            ZipCode = "10001"
        }
    };

    private readonly XmlSerializerService _xmlSerializerService = new();

    [Fact]
    public void Serialize_ShouldReturnValidXmlString_WhenObjectIsNotNull()
    {
        // Act
        var xmlString = _xmlSerializerService.Serialize(_person);

        // Assert
        Assert.NotNull(xmlString);
        Assert.StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\"?>", xmlString);
        Assert.Contains("<Person>", xmlString);
        Assert.Contains("<Name>Alice</Name>", xmlString);
        Assert.Contains("<Age>25</Age>", xmlString);
        Assert.Contains("<Address>", xmlString);
        Assert.Contains("<Street>Main Street</Street>", xmlString);
        Assert.Contains("<City>New York</City>", xmlString);
        Assert.Contains("<ZipCode>10001</ZipCode>", xmlString);
        Assert.Contains("</Address>", xmlString);
        Assert.Contains("</Person>", xmlString);
    }

    [Fact]
    public void Serialize_ShouldThrowArgumentNullException_WhenObjectIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() => _xmlSerializerService.Serialize<Person>(null!));
    }

    [XmlRoot("Person")]
    public class Person
    {
        [XmlElement("Name")]
        public string Name { get; set; } = null!;

        [XmlElement("Age")]
        public int Age { get; set; }

        [XmlElement("Address")]
        public Address Address { get; set; } = null!;
    }

    public class Address
    {
        [XmlElement("Street")]
        public string Street { get; set; } = null!;

        [XmlElement("City")]
        public string City { get; set; } = null!;

        [XmlElement("ZipCode")]
        public string ZipCode { get; set; } = null!;
    }
}
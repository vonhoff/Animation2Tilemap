namespace Animation2Tilemap.Services.Contracts;

public interface IXmlSerializerService
{
    string Serialize<T>(T obj) where T : class;
}
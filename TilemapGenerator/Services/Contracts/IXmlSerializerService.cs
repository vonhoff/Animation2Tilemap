namespace TilemapGenerator.Services.Contracts;

public interface IXmlSerializerService
{
    string Serialize<T>(T obj) where T : class;
}
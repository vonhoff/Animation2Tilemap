using TilemapGenerator.Entities;

namespace TilemapGenerator.Services.Contracts;

public interface ITilemapSerializerService
{
    string Serialize(Tilemap tilemap);
}
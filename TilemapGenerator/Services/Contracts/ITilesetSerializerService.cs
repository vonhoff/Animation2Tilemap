using TilemapGenerator.Entities;

namespace TilemapGenerator.Services.Contracts
{
    public interface ITilesetSerializerService
    {
        string Serialize(Tileset tileset);
    }
}
using TilemapGenerator.Enums;

namespace TilemapGenerator.Services.Contracts;

public interface ITilemapDataService
{
    List<uint> ParseData(string input, TileLayerFormat format);

    string SerializeData(List<uint> data, TileLayerFormat format);
}
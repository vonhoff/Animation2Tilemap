using Animation2Tilemap.Enums;

namespace Animation2Tilemap.Services.Contracts;

public interface ITilemapDataService
{
    List<uint> ParseData(string input, TileLayerFormat format);

    string SerializeData(List<uint> data, TileLayerFormat format);
}
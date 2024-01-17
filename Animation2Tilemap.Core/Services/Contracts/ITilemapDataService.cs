using Animation2Tilemap.Core.Enums;

namespace Animation2Tilemap.Core.Services.Contracts;

public interface ITilemapDataService
{
    List<uint> ParseData(string input, TileLayerFormat format);

    string SerializeData(uint[] data, TileLayerFormat format);
}
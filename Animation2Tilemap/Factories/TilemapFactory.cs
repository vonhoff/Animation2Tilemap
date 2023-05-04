using Animation2Tilemap.Entities;
using Animation2Tilemap.Enums;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Factories;

public class TilemapFactory : ITilemapFactory
{
    private readonly ITilemapDataService _tilemapDataService;
    private readonly TileLayerFormat _tileLayerFormat;

    public TilemapFactory(
        ApplicationOptions options,
        ITilemapDataService tilemapDataService)
    {
        _tilemapDataService = tilemapDataService;
        _tileLayerFormat = options.TileLayerFormat;
    }

    /// <summary>
    /// Creates a tilemap using the given tileset.
    /// </summary>
    /// <param name="tileset">The tileset to use.</param>
    /// <returns>The created tilemap.</returns>
    public Tilemap CreateFromTileset(Tileset tileset)
    {
        var hashToTileId = tileset.RegisteredTiles
            .Where(t => t.Animation?.Hash != null)
            .ToDictionary(t => t.Animation!.Hash, t => (uint)t.Id + 1);

        var mapData = new uint[tileset.HashAccumulations.Count];
        var i = 0;
        foreach (var hashAccumulation in tileset.HashAccumulations.Values)
        {
            if (hashToTileId.TryGetValue(hashAccumulation, out var tileId))
            {
                mapData[i] = tileId;
            }
            i++;
        }

        var layerData = _tilemapDataService.SerializeData(mapData, _tileLayerFormat);
        var width = tileset.OriginalSize.Width / tileset.TileWidth;
        var height = tileset.OriginalSize.Height / tileset.TileHeight;

        var encoding = _tileLayerFormat is
            TileLayerFormat.Base64Uncompressed or
            TileLayerFormat.Base64GZip or
            TileLayerFormat.Base64ZLib ?
            "base64" : "csv";

        var compression = _tileLayerFormat switch
        {
            TileLayerFormat.Base64GZip => "gzip",
            TileLayerFormat.Base64ZLib => "zlib",
            _ => null
        };

        return new Tilemap
        {
            Version = "1.0",
            Orientation = "orthogonal",
            RenderOrder = "right-down",
            Width = width,
            Height = height,
            TileWidth = tileset.TileWidth,
            TileHeight = tileset.TileHeight,
            NextObjectId = 1,
            Tileset = new TilemapTileset
            {
                FirstGid = 1,
                Source = tileset.Name + ".tsx"
            },
            TilemapLayer = new TilemapLayer
            {
                Id = 1,
                Name = "Tile Layer 1",
                Width = width,
                Height = height,
                Data = new TilemapLayerData
                {
                    Encoding = encoding,
                    Compression = compression,
                    Text = layerData
                }
            }
        };
    }
}
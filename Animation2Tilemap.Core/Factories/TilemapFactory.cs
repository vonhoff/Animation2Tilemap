using Animation2Tilemap.Core.Entities;
using Animation2Tilemap.Core.Enums;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Services.Contracts;
using Animation2Tilemap.Core.Workflows;

namespace Animation2Tilemap.Core.Factories;

public class TilemapFactory(MainWorkflowOptions options, ITilemapDataService tilemapDataService) : ITilemapFactory
{
    private readonly TileLayerFormat _tileLayerFormat = options.TileLayerFormat;

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

        var layerData = tilemapDataService.SerializeData(mapData, _tileLayerFormat);
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
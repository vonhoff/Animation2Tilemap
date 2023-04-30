using TilemapGenerator.Entities;
using TilemapGenerator.Enums;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilemapFactory : ITilemapFactory
{
    private readonly ITilemapDataService _tilemapDataService;

    public TilemapFactory(ITilemapDataService tilemapDataService)
    {
        _tilemapDataService = tilemapDataService;
    }

    public Tilemap CreateFromTileset(Tileset tileset)
    {
        var mapData = new List<uint>(tileset.HashAccumulations.Count);
        for (var y = 0; y < tileset.OriginalSize.Height; y += tileset.TileHeight)
        {
            for (var x = 0; x < tileset.OriginalSize.Width; x += tileset.TileWidth)
            {
                var tileLocation = new Point(x, y);
                var hashAccumulation = tileset.HashAccumulations[tileLocation];
                var tilesetTile = tileset.RegisteredTiles.Find(t => t.Animation?.Hash == hashAccumulation);
                if (tilesetTile != null)
                {
                    mapData.Add((uint)tilesetTile.Id + 1);
                }
            }
        }

        var layerData = _tilemapDataService.SerializeData(mapData, TileLayerFormat.Base64GZip);
        return new Tilemap
        {
            Version = "1.0",
            Orientation = "orthogonal",
            RenderOrder = "right-down",
            Width = tileset.OriginalSize.Width / tileset.TileWidth,
            Height = tileset.OriginalSize.Height / tileset.TileHeight,
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
                Width = tileset.OriginalSize.Width / tileset.TileWidth,
                Height = tileset.OriginalSize.Height / tileset.TileHeight,
                Data = new TilemapLayerData
                {
                    Encoding = "base64",
                    Compression = "zlib",
                    Text = layerData
                }
            }
        };
    }
}
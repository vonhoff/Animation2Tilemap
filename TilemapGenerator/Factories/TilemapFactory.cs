﻿using TilemapGenerator.Entities;
using TilemapGenerator.Enums;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilemapFactory : ITilemapFactory
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

    public Tilemap CreateFromTileset(Tileset tileset)
    {
        var mapData = new List<uint>(tileset.HashAccumulations.Count);
        for (var y = 0; y < tileset.OriginalSize.Height; y += tileset.TileHeight)
        {
            for (var x = 0; x < tileset.OriginalSize.Width; x += tileset.TileWidth)
            {
                var tileLocation = new Point(x, y);
                var hashAccumulation = tileset.HashAccumulations[tileLocation];
                var tilesetTile = tileset.RegisteredTiles.FirstOrDefault(t => t.Animation?.Hash == hashAccumulation);
                if (tilesetTile != null)
                {
                    mapData.Add((uint)tilesetTile.Id + 1);
                }
            }
        }

        var layerData = _tilemapDataService.SerializeData(mapData, TileLayerFormat.Base64GZip);
        var width = tileset.OriginalSize.Width / tileset.TileWidth;
        var height = tileset.OriginalSize.Height / tileset.TileHeight;

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
                    Encoding = "base64",
                    Compression = "zlib",
                    Text = layerData
                }
            }
        };
    }
}
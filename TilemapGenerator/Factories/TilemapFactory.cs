using TilemapGenerator.Entities;
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
        return new Tilemap
        {
            Version = "1.0",
            Orientation = "orthogonal",
            RenderOrder = "right-down",
            Width = tileset.Image.Width,
            Height = tileset.Image.Height,
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
                Width = tileset.Image.Width,
                Height = tileset.Image.Height,
            }
        };
    }
}
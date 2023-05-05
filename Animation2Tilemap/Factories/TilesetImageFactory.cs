﻿using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories.Contracts;

namespace Animation2Tilemap.Factories;

public class TilesetImageFactory : ITilesetImageFactory
{
    private readonly Size _tileSize;

    public TilesetImageFactory(ApplicationOptions options)
    {
        _tileSize = options.TileSize;
    }

    public TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName)
    {
        var numTiles = registeredTiles.Count;
        var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
        var numRows = (int)Math.Ceiling((double)numTiles / numCols);
        var outputImage = new Image<Rgba32>(numCols * _tileSize.Width, numRows * _tileSize.Height);

        for (var y = 0; y < numCols; y++)
        {
            for (var x = 0; x < numRows; x++)
            {
                var tileIndex = y * numCols + x;
                if (tileIndex >= numTiles)
                {
                    continue;
                }

                var tile = registeredTiles[tileIndex];
                var x1 = x * _tileSize.Width;
                var y1 = y * _tileSize.Height;
                outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));
            }
        }

        var tilesetImage = new TilesetImage
        {
            Path = fileName,
            Data = outputImage,
            Width = outputImage.Width,
            Height = outputImage.Height
        };

        return tilesetImage;
    }
}
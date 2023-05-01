using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;

namespace TilemapGenerator.Factories;

public sealed class TilesetImageFactory : ITilesetImageFactory
{
    private readonly Size _tileSize;

    public TilesetImageFactory(ApplicationOptions options)
    {
        _tileSize = options.TileSize;
    }

    /// <summary>
    /// Creates a TilesetImage from a list of TilesetTiles.
    /// </summary>
    /// <param name="registeredTiles">The list of TilesetTiles to use for the TilesetImage.</param>
    /// <param name="fileName">The name of the file to save the TilesetImage to.</param>
    /// <returns>The created TilesetImage.</returns>
    public TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName)
    {
        var numTiles = registeredTiles.Count;
        var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
        var numRows = (int)Math.Ceiling((double)numTiles / numCols);
        var outputImage = new Image<Rgba32>(numCols * _tileSize.Width, numRows * _tileSize.Height);

        Parallel.For(0, numRows, y =>
        {
            Parallel.For(0, numCols, x =>
            {
                var tileIndex = y * numCols + x;
                if (tileIndex >= numTiles)
                {
                    return;
                }

                var tile = registeredTiles[tileIndex];
                var x1 = x * _tileSize.Width;
                var y1 = y * _tileSize.Height;
                outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));
            });
        });

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
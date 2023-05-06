using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories.Contracts;

namespace Animation2Tilemap.Factories;

public class TilesetImageFactory : ITilesetImageFactory
{
    private readonly Size _tileSize;
    private readonly Rgba32 _transparentColor;

    public TilesetImageFactory(ApplicationOptions options)
    {
        _tileSize = options.TileSize;
        _transparentColor = options.TransparentColor;
    }

    public TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName)
    {
        var numTiles = registeredTiles.Count;
        var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
        var numRows = (int)Math.Ceiling((double)numTiles / numCols);
        var outputImage = new Image<Rgba32>(numCols * _tileSize.Width, numRows * _tileSize.Height);

        var x = 0;
        var y = 0;
        foreach (var tile in registeredTiles)
        {
            var x1 = x;
            var y1 = y;
            outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));

            x += _tileSize.Width;
            if (x < outputImage.Width)
            {
                continue;
            }

            x = 0;
            y += _tileSize.Height;
        }

        var tilesetImage = new TilesetImage
        {
            Path = fileName,
            Trans = _transparentColor.ToHex(),
            Width = outputImage.Width,
            Height = outputImage.Height,
            Data = outputImage
        };

        return tilesetImage;
    }
}
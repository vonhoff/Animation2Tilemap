using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories.Contracts;

namespace Animation2Tilemap.Factories;

public class TilesetImageFactory : ITilesetImageFactory
{
    private readonly Rgba32 _transparentColor;
    private readonly Size _tileSize;
    private readonly int _tileSpacing;

    public TilesetImageFactory(ApplicationOptions options)
    {
        _tileSize = options.TileSize;
        _transparentColor = options.TransparentColor;
        _tileSpacing = options.TileSpacing;
    }

    public TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName)
    {
        var numTiles = registeredTiles.Count;
        var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
        var numRows = (int)Math.Ceiling((double)numTiles / numCols);

        var outputImageWidth = numCols * (_tileSize.Width + _tileSpacing) + 2 * _tileSpacing;
        var outputImageHeight = numRows * (_tileSize.Height + _tileSpacing) + 2 * _tileSpacing;

        var outputImage = new Image<Rgba32>(outputImageWidth, outputImageHeight);
        outputImage.Mutate(context => context.BackgroundColor(_transparentColor));

        var x = 0;
        var y = 0;
        foreach (var tile in registeredTiles)
        {
            var x1 = x;
            var y1 = y;
            outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));

            x += _tileSize.Width + _tileSpacing;
            if (x < outputImage.Width)
            {
                continue;
            }

            x = 0;
            y += _tileSize.Height + _tileSpacing;
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
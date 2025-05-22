using Animation2Tilemap.Entities;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Workflows;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Animation2Tilemap.Factories;

public class TilesetImageFactory : ITilesetImageFactory
{
    private readonly int _tileMargin;
    private readonly Size _tileSize;
    private readonly int _tileSpacing;
    private readonly Rgba32 _transparentColor;

    public TilesetImageFactory(MainWorkflowOptions options)
    {
        _tileMargin = options.TileMargin;
        _tileSize = options.TileSize;
        _tileSpacing = options.TileSpacing;
        _transparentColor = options.TransparentColor;
    }

    public TilesetImage CreateFromTiles(IReadOnlyList<TilesetTile> registeredTiles, string fileName)
    {
        var numTiles = registeredTiles.Count;
        var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
        var numRows = (int)Math.Ceiling((double)numTiles / numCols);

        var outputImageWidth = numCols * (_tileSize.Width + _tileSpacing) + _tileMargin - _tileSpacing;
        var outputImageHeight = numRows * (_tileSize.Height + _tileSpacing) + _tileMargin - _tileSpacing;

        var outputImage = new Image<Rgba32>(outputImageWidth, outputImageHeight);
        outputImage.Mutate(context => context.BackgroundColor(_transparentColor));

        var x = _tileMargin;
        var y = _tileMargin;
        foreach (var tile in registeredTiles)
        {
            var x1 = x;
            var y1 = y;
            outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));

            x += _tileSize.Width + _tileSpacing;
            if (x < outputImage.Width) continue;

            x = _tileMargin;
            y += _tileSize.Height + _tileSpacing;
        }

        var tilesetImage = new TilesetImage
        {
            Path = fileName + ".png",
            Trans = _transparentColor.ToHex(),
            Width = outputImage.Width,
            Height = outputImage.Height,
            Data = outputImage
        };

        return tilesetImage;
    }
}
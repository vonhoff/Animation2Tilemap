using TilemapGenerator.Common.Configuration;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;

namespace TilemapGenerator.Factories
{
    public class TilesetImageFactory : ITilesetImageFactory
    {
        private readonly Size _tileSize;

        public TilesetImageFactory(ApplicationOptions options)
        {
            _tileSize = options.TileSize;
        }

        public TilesetImage CreateFromTiles(List<TilesetTile> tileRegister, string fileName)
        {
            var numTiles = tileRegister.Count;
            var numCols = (int)Math.Ceiling(Math.Sqrt(numTiles));
            var numRows = (int)Math.Ceiling((double)numTiles / numCols);

            var outputImage = new Image<Rgba32>(numCols * _tileSize.Width, numRows * _tileSize.Height);

            var x = 0;
            var y = 0;
            foreach (var tile in tileRegister)
            {
                var x1 = x;
                var y1 = y;
                outputImage.Mutate(ctx => ctx.DrawImage(tile.Image.Data, new Point(x1, y1), 1f));

                x += _tileSize.Width;
                if (x >= outputImage.Width)
                {
                    x = 0;
                    y += _tileSize.Height;
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
}
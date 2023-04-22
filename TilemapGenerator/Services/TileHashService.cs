using TilemapGenerator.Common.CommandLine;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class TileHashService : ITileHashService
    {
        private readonly Size _tileSize;

        public TileHashService(CommandLineOptions options)
        {
            _tileSize = options.TileSize;
        }

        private const int Prime1 = 486187739;
        private const int Prime2 = 76624727;
        private const int Prime3 = 179424673;
        private const int Prime4 = 668265263;
        private const int Prime5 = 374761393;
        private const int Prime6 = 258915779;

        public int Compute(Image<Rgba32> image, int x, int y)
        {
            var hash = Prime1;

            for (var tileX = x; tileX < x + _tileSize.Width; tileX++)
            {
                for (var tileY = y; tileY < y + _tileSize.Height; tileY++)
                {
                    var pixelColor = image[tileX, tileY];
                    unchecked
                    {
                        hash = (hash * Prime1) ^ pixelColor.R;
                        hash = (hash * Prime2) ^ pixelColor.G;
                        hash = (hash * Prime3) ^ pixelColor.B;
                        hash = (hash * Prime4) ^ pixelColor.A;
                        hash = (hash * Prime5) ^ tileX;
                        hash = (hash * Prime6) ^ tileY;
                    }
                }
            }

            return hash;
        }
    }
}
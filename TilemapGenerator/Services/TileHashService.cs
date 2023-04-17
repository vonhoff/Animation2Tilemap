using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class TileHashService : ITileHashService
    {
        private const int Prime1 = 486187739;
        private const int Prime2 = 76624727;
        private const int Prime3 = 179424673;
        private const int Prime4 = 668265263;
        private const int Prime5 = 374761393;
        private const int Prime6 = 258915779;
        private const int Prime7 = 58864111;

        public int Compute(Rgba32 pixelColor, int baseValue, int tileX, int tileY)
        {
            unchecked
            {
                var hash = Prime1;
                hash = (hash * Prime1) ^ pixelColor.R;
                hash = (hash * Prime2) ^ pixelColor.G;
                hash = (hash * Prime3) ^ pixelColor.B;
                hash = (hash * Prime4) ^ pixelColor.A;
                hash = (hash * Prime5) ^ baseValue;
                hash = (hash * Prime6) ^ tileX;
                hash = (hash * Prime7) ^ tileY;
                return hash;
            }
        }
    }
}
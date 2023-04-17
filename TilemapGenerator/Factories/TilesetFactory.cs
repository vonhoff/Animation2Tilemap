using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Factories
{
    public class TilesetFactory : ITilesetFactory
    {
        private readonly ITileHashService _hashService;

        public TilesetFactory(ITileHashService hashService)
        {
            _hashService = hashService;
        }

        public Tileset FromFrames(List<Image<Rgba32>> frames, Size tileSize)
        {
            throw new NotImplementedException();
        }

        public Tileset FromFramesConsolidated(List<Image<Rgba32>> frames, Size tileSize)
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, Point> GetUniqueTiles(Image<Rgba32> inputImage, Size tileSize)
        {
            var uniqueTiles = new Dictionary<int, Point>();

            for (var x = 0; x < inputImage.Width; x += tileSize.Width)
            {
                for (var y = 0; y < inputImage.Height; y += tileSize.Height)
                {
                    var tileHash = 17;

                    for (var tileX = x; tileX < x + tileSize.Width; tileX++)
                    {
                        for (var tileY = y; tileY < y + tileSize.Height; tileY++)
                        {
                            var pixelColor = inputImage[tileX, tileY];
                            tileHash = _hashService.Compute(pixelColor, tileHash, tileX, tileY);
                        }
                    }

                    if (!uniqueTiles.ContainsKey(tileHash))
                    {
                        uniqueTiles.Add(tileHash, new Point(x, y));
                    }
                }
            }

            return uniqueTiles;
        }
    }
}
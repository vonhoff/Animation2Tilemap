using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public class ImageHashService : IImageHashService
{
    private const int Prime1 = 486187739;
    private const int Prime2 = 76624727;
    private const int Prime3 = 179424673;
    private const int Prime4 = 668265263;
    private const int Prime5 = 374761393;
    private const int Prime6 = 258915779;
    private readonly Size _tileSize;

    public ImageHashService(ApplicationOptions options)
    {
        _tileSize = options.TileSize;
    }

    /// <summary>
    /// Computes a hash value for an image using the pixel colors and tile position.
    /// </summary>
    /// <param name="image">The image to compute the hash value for.</param>
    /// <returns>The hash value computed for the image.</returns>
    public int Compute(Image<Rgba32> image)
    {
        var hash = Prime1;

        for (var tileX = 0; tileX < _tileSize.Width; tileX++)
        {
            for (var tileY = 0; tileY < _tileSize.Height; tileY++)
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
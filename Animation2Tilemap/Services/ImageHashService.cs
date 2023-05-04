using Animation2Tilemap.Services.Contracts;
using SixLabors.ImageSharp.Advanced;

namespace Animation2Tilemap.Services;

public class ImageHashService : IImageHashService
{
    private const uint Prime1 = 4294967291u; // largest prime less than 2^32
    private const uint Prime2 = 31u;
    private const uint Prime3 = 37u;
    private const uint Prime4 = 41u;
    private const uint Prime5 = 43u;
    private const uint Prime6 = 47u;

    public uint Compute(Image<Rgba32> image)
    {
        var hash = Prime1;
        var x = 0u;
        var y = 0u;
        foreach (var memory in image.Frames.RootFrame.GetPixelMemoryGroup())
        {
            foreach (var pixel in memory.Span)
            {
                hash = hash * Prime1 + pixel.R;
                hash = hash * Prime2 + pixel.G;
                hash = hash * Prime3 + pixel.B;
                hash = hash * Prime4 + pixel.A;
                hash = hash * Prime5 + x;
                hash = hash * Prime6 + y;
                x++;
            }
            y++;
            x = 0;
        }

        return hash;
    }
}
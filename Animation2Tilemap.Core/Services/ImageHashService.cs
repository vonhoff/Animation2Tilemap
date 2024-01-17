using Animation2Tilemap.Core.Services.Contracts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Core.Services;

public class ImageHashService : IImageHashService
{
    private const uint Prime1 = 4294967291u; // largest prime less than 2^32
    private const uint Prime2 = 31u;
    private const uint Prime3 = 37u;
    private const uint Prime4 = 41u;
    private const uint Prime5 = 43u;
    private const uint Prime6 = 47u;
    private const uint Prime7 = 53u;

    public uint Compute(Image<Rgba32> image)
    {
        var memoryGroup = image.Frames.RootFrame.GetPixelMemoryGroup();
        var hash = Prime1;
        var x = 0u;
        var y = 0u;
        foreach (var memory in memoryGroup)
        {
            foreach (var pixel in memory.Span)
            {
                hash = hash * Prime2 + pixel.R;
                hash = hash * Prime3 + pixel.G;
                hash = hash * Prime4 + pixel.B;
                hash = hash * Prime5 + pixel.A;
                hash = hash * Prime6 + x;
                hash = hash * Prime7 + y;
                x++;
            }
            y++;
            x = 0;
        }

        return hash;
    }
}
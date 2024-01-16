using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Services.Contracts;

public interface IImageHashService
{
    uint Compute(Image<Rgba32> image);
}
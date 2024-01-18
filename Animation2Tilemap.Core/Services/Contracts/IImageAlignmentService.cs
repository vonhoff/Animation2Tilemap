using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Core.Services.Contracts;

public interface IImageAlignmentService
{
    bool TryAlignImage(string fileName, List<Image<Rgba32>> frames);
}
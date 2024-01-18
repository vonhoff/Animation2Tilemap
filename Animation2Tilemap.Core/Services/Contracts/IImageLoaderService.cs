using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Core.Services.Contracts;

public interface IImageLoaderService
{
    bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images);
}
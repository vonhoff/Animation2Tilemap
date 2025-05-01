using Animation2Tilemap.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Factories.Contracts;

public interface ITilesetFactory : IFrameProcessingNotifier
{
    Tileset CreateFromImage(string fileName, List<Image<Rgba32>> frames);
}
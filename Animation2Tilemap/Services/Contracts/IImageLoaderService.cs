namespace Animation2Tilemap.Services.Contracts;

public interface IImageLoaderService
{
    bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images);
}
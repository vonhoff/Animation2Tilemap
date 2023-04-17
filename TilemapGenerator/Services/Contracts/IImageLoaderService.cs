namespace TilemapGenerator.Services.Contracts
{
    public interface IImageLoaderService
    {
        bool TryLoadImages(string path, out Dictionary<string, List<Image<Rgba32>>> images, out bool suitableForAnimation);

        Dictionary<string, List<Image<Rgba32>>> LoadFromDirectory(string path, out bool suitableForAnimation);

        List<Image<Rgba32>> LoadFromFile(string file);
    }
}
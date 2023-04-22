namespace TilemapGenerator.Services.Contracts
{
    public interface IImageLoaderService
    {
        bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images);
    }
}
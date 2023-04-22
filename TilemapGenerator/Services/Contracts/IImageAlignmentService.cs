namespace TilemapGenerator.Services.Contracts
{
    public interface IImageAlignmentService
    {
        void AlignCollection(Dictionary<string, List<Image<Rgba32>>> images, Size tileSize, Rgba32 transparentColor);
    }
}
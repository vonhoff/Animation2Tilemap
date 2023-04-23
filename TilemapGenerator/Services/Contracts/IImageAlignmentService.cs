namespace TilemapGenerator.Services.Contracts
{
    public interface IImageAlignmentService
    {
        void AlignImageCollection(Dictionary<string, List<Image<Rgba32>>> images);
    }
}
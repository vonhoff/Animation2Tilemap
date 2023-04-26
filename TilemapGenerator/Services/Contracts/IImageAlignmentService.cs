namespace TilemapGenerator.Services.Contracts
{
    public interface IImageAlignmentService
    {
        bool TryAlignImage(string fileName, List<Image<Rgba32>> frames);
    }
}
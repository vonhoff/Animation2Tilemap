namespace TilemapGenerator.Contracts
{
    public interface IImageAlignmentService
    {
        Image<Rgba32> AlignFrame(Image<Rgba32> frame, Size tileSize, Rgba32 backgroundColor);
    }
}
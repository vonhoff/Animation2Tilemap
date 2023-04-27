namespace TilemapGenerator.Services.Contracts
{
    public interface IImageHashService
    {
        int Compute(Image<Rgba32> image);
    }
}
namespace TilemapGenerator.Services.Contracts
{
    public interface ITileHashService
    {
        int Compute(Image<Rgba32> image);
    }
}
namespace TilemapGenerator.Services.Contracts
{
    public interface ITileHashService
    {
        int Compute(Image<Rgba32> image, Size tileSize, int x, int y);
    }
}
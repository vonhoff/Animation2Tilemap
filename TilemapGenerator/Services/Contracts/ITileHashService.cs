namespace TilemapGenerator.Services.Contracts
{
    public interface ITileHashService
    {
        int Compute(Rgba32 pixelColor, int baseValue, int tileX, int tileY);
    }
}
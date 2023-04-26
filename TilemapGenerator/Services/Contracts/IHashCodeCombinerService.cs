namespace TilemapGenerator.Services.Contracts
{
    public interface IHashCodeCombinerService
    {
        int CombineHashCodes(int h1, int h2);
    }
}
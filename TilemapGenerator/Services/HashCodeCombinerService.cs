using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class HashCodeCombinerService : IHashCodeCombinerService
    {
        public int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }
    }
}
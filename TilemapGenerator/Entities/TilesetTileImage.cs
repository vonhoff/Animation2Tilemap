namespace TilemapGenerator.Entities
{
    public class TilesetTileImage : IEquatable<TilesetTileImage>
    {
        public TilesetTileImage(Image<Rgba32> data, int hash)
        {
            Data = data;
            Hash = hash;
        }

        public Image<Rgba32> Data { get; }

        public int Hash { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as TilesetTileImage);
        }

        public bool Equals(TilesetTileImage? other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            return Hash == other.Hash;
        }

        public override int GetHashCode()
        {
            return Hash;
        }
    }
}
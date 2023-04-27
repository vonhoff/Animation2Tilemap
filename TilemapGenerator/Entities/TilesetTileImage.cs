namespace TilemapGenerator.Entities
{
    public class TilesetTileImage : IEquatable<TilesetTileImage>
    {
        private readonly int _hash;

        public TilesetTileImage(Image<Rgba32> data, int hash)
        {
            Data = data;
            _hash = hash;
        }

        public Image<Rgba32> Data { get; }

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

            return _hash == other._hash;
        }

        public override int GetHashCode()
        {
            return _hash;
        }
    }
}
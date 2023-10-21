namespace Animation2Tilemap.Entities;

public readonly struct TilesetTileImage : IEquatable<TilesetTileImage>
{
    public TilesetTileImage(Image<Rgba32> data, uint hash)
    {
        Data = data;
        Hash = hash;
    }

    public Image<Rgba32> Data { get; }

    public uint Hash { get; }

    public static bool operator !=(TilesetTileImage left, TilesetTileImage right)
    {
        return !(left == right);
    }

    public static bool operator ==(TilesetTileImage left, TilesetTileImage right)
    {
        return left.Equals(right);
    }

    public bool Equals(TilesetTileImage other)
    {
        return other.Hash == Hash;
    }

    public override bool Equals(object? obj)
    {
        if (obj is TilesetTileImage other)
        {
            return Equals(other);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Hash.GetHashCode();
    }
}
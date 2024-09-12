using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Core.Entities;

public readonly struct TilesetTileImage : IEquatable<TilesetTileImage>
{
    public Image<Rgba32> Data { get; }

    private readonly uint _hash;

    public TilesetTileImage(Image<Rgba32> data, uint hash)
    {
        Data = data;
        _hash = hash;
    }

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
        return other._hash == _hash;
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
        return _hash.GetHashCode();
    }
}
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

    public bool Equals(TilesetTileImage other)
    {
        return other.Hash == Hash;
    }
}
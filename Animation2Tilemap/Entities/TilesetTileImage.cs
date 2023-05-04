namespace Animation2Tilemap.Entities;

public readonly struct TilesetTileImage
{
    public TilesetTileImage(Image<Rgba32> data, uint hash)
    {
        Data = data;
        Hash = hash;
    }

    public Image<Rgba32> Data { get; }
    public uint Hash { get; }

    public override bool Equals(object? obj)
    {
        if (obj is TilesetTileImage tileImage)
        {
            return tileImage.Hash == Hash;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (int)Hash;
    }
}
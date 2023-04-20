namespace TilemapGenerator.Records
{
    public record TileRecord(int Id, Point Location, int Hash, Image<Rgba32> ImageData);
}
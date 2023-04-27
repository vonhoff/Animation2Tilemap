namespace TilemapGenerator.Services.Contracts
{
    public interface INamePatternService
    {
        string? GetMostNotablePattern(List<string> names);
    }
}
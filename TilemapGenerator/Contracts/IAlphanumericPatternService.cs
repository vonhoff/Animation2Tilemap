namespace TilemapGenerator.Contracts
{
    public interface IAlphanumericPatternService
    {
        string? GetMostOccurringPattern(List<string> strings);

        string? GetMostOccurringLetter(List<string> strings);
    }
}
namespace Animation2Tilemap.Services.Contracts;

public interface INamePatternService
{
    string? GetMostNotablePattern(List<string> names);
}
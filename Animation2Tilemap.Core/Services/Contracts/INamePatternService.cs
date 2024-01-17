namespace Animation2Tilemap.Core.Services.Contracts;

public interface INamePatternService
{
    string? GetMostNotablePattern(List<string> names);
}
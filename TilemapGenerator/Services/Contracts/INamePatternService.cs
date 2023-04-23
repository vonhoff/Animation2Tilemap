﻿namespace TilemapGenerator.Services.Contracts
{
    public interface INamePatternService
    {
        string? GetMostOccurringPattern(List<string> strings);

        string? GetMostOccurringLetter(List<string> strings);
    }
}
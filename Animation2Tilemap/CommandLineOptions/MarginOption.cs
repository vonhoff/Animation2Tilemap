﻿using System.CommandLine;

namespace Animation2Tilemap.CommandLineOptions;

public class MarginOption
{
    public MarginOption()
    {
        Option = new Option<int>(
            name: "--margin",
            description: "Tile margin",
            getDefaultValue: () => 0);
        Option.AddAlias("-m");
    }

    public Option<int> Option { get; }

    public Option<int> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            int margin;
            try
            {
                margin = optionResult?.GetValueOrDefault<int>() ?? 0;
            }
            catch (InvalidOperationException)
            {
                margin = 0;
            }

            if (margin < 0)
            {
                result.ErrorMessage = $"Invalid margin '{margin}'. Margin should be equal or greater than 0.";
            }
        });
        return Option;
    }
}
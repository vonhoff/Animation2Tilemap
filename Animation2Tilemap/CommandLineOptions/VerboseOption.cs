﻿using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public sealed class VerboseOption : ICommandLineOption<bool>
{
    public VerboseOption()
    {
        Option = new Option<bool>(
            name: "--verbose",
            description: "Enable verbose logging",
            getDefaultValue: () => false);
        Option.AddAlias("-v");
    }

    public Option<bool> Option { get; }

    public Option<bool> Register(Command command)
    {
        command.Add(Option);
        return Option;
    }
}
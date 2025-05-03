using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public class AssumeAnimationOption : ICommandLineOption<bool>
{
    public AssumeAnimationOption()
    {
        Option = new Option<bool>(
            name: "--assume-animation",
            description: "Assume images in an input directory matching animation patterns are frames, bypassing the confirmation prompt",
            getDefaultValue: () => false);
    }

    public Option<bool> Option { get; }

    public Option<bool> Register(Command command)
    {
        command.Add(Option);
        return Option;
    }
}
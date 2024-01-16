using System.CommandLine;

namespace Animation2Tilemap.CommandLineOptions;

public class VerboseOption
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
using System.CommandLine;
using TilemapGenerator.CommandLineOptions.Contracts;

namespace TilemapGenerator.CommandLineOptions;

public sealed class InputOption : ICommandLineOption<string>
{
    public InputOption()
    {
        Option = new Option<string>(
            name: "--input",
            description: "Input file or folder path");
        Option.AddAlias("-i");
        Option.IsRequired = true;
    }

    public Option<string> Option { get; }

    public Option<string> Register(Command command)
    {
        command.Add(Option);
        return Option;
    }
}
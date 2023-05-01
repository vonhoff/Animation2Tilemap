using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public sealed class HeightOption : ICommandLineOption<int>
{
    public HeightOption()
    {
        Option = new Option<int>(
            name: "--height",
            description: "Tile height",
            getDefaultValue: () => 8);
        Option.AddAlias("-h");
    }

    public Option<int> Option { get; }

    public Option<int> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            int height;
            try
            {
                height = optionResult?.GetValueOrDefault<int>() ?? 0;
            }
            catch (InvalidOperationException)
            {
                height = 0;
            }

            if (height <= 0)
            {
                result.ErrorMessage = $"Invalid height '{height}'. Height should be greater than 0.";
            }
        });
        return Option;
    }
}
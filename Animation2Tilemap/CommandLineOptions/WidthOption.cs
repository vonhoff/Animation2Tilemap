using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public class WidthOption : ICommandLineOption<int>
{
    public WidthOption()
    {
        Option = new Option<int>(
            "--width",
            description: "Tile width",
            getDefaultValue: () => 8);
        Option.AddAlias("-w");
    }

    public Option<int> Option { get; }

    public Option<int> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            int width;
            try
            {
                width = optionResult?.GetValueOrDefault<int>() ?? 0;
            }
            catch (InvalidOperationException)
            {
                width = 0;
            }

            if (width <= 0) result.ErrorMessage = $"Invalid width '{width}'. Width should be greater than 0.";
        });
        return Option;
    }
}
using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public class SpacingOption : ICommandLineOption<int>
{
    public SpacingOption()
    {
        Option = new Option<int>(
            "--spacing",
            description: "Tile spacing",
            getDefaultValue: () => 0);
        Option.AddAlias("-s");
    }

    public Option<int> Option { get; }

    public Option<int> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            int spacing;
            try
            {
                spacing = optionResult?.GetValueOrDefault<int>() ?? 0;
            }
            catch (InvalidOperationException)
            {
                spacing = 0;
            }

            if (spacing < 0)
                result.ErrorMessage = $"Invalid spacing '{spacing}'. Spacing should be equal or greater than 0.";
        });
        return Option;
    }
}
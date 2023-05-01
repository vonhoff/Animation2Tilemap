using System.CommandLine;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public sealed class FrameDurationOption : ICommandLineOption<int>
{
    public FrameDurationOption()
    {
        Option = new Option<int>(
            name: "--duration",
            description: "Animation frame duration",
            getDefaultValue: () => 125);
        Option.AddAlias("-d");
    }

    public Option<int> Option { get; }

    public Option<int> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            int? duration;
            try
            {
                duration = optionResult?.GetValueOrDefault<int>();
            }
            catch (InvalidOperationException)
            {
                duration = null;
            }

            if (duration is not > 0)
            {
                result.ErrorMessage = $"Invalid frame duration '{duration}'. Animation frame duration should be greater than 0.";
            }
        });
        return Option;
    }
}
using Animation2Tilemap.Console.CommandLineOptions.Contracts;
using System.CommandLine;

namespace Animation2Tilemap.Console.CommandLineOptions;

public class TileLayerFormatOption : ICommandLineOption<string>
{
    public TileLayerFormatOption()
    {
        Option = new Option<string>(
            name: "--format",
            description: "Tile layer format",
            getDefaultValue: () => "zlib");
        Option.AddAlias("-f");
        Option.ArgumentHelpName = "base64|zlib|gzip|csv";
    }

    public Option<string> Option { get; }

    public Option<string> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var availableOptions = Option.ArgumentHelpName!.Split("|");
            var optionResult = result.FindResultFor(Option);
            string? format;
            try
            {
                format = optionResult?.GetValueOrDefault<string>()?.ToLowerInvariant();
            }
            catch (InvalidOperationException)
            {
                format = null;
            }

            if (format == null || availableOptions.Contains(format) == false)
            {
                result.ErrorMessage = $"Invalid format '{format}'. " +
                                      $"Format must be one of the following options: {string.Join(", ", availableOptions)}";
            }
        });
        return Option;
    }
}
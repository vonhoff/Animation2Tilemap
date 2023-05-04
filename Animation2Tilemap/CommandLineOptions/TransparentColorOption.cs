using System.CommandLine;
using System.Text.RegularExpressions;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap.CommandLineOptions;

public partial class TransparentColorOption : ICommandLineOption<string>
{
    public TransparentColorOption()
    {
        Option = new Option<string>(
            name: "--transparent",
            description: "Transparent color (RGBA)",
            getDefaultValue: () => "00000000");
        Option.AddAlias("-t");
    }

    public Option<string> Option { get; }

    public Option<string> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            string? transparentColor;
            try
            {
                transparentColor = optionResult?.GetValueOrDefault<string>();
            }
            catch (InvalidOperationException)
            {
                transparentColor = null;
            }

            if (string.IsNullOrEmpty(transparentColor) || !RgbaColorValidationRegex().IsMatch(transparentColor))
            {
                result.ErrorMessage = $"Invalid transparent color '{transparentColor}'. Transparent color must be a valid RGBA color string.";
            }
        });
        return Option;
    }

    [GeneratedRegex("^([0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$")]
    private static partial Regex RgbaColorValidationRegex();
}
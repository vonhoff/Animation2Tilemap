using System.CommandLine;
using Animation2Tilemap.Console.CommandLineOptions.Contracts;

namespace Animation2Tilemap.Console.CommandLineOptions;

public class InputOption : ICommandLineOption<string>
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
        command.AddValidator(result =>
        {
            var inputResult = result.FindResultFor(Option);
            if (inputResult == null)
            {
                return;
            }

            var inputPath = inputResult.GetValueOrDefault<string>();
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                result.ErrorMessage = "The input path cannot be empty.";
                return;
            }

            if (File.Exists(inputPath) == false && Directory.Exists(inputPath) == false)
            {
                result.ErrorMessage = $"The input path '{inputPath}' does not exist.";
            }
        });
        return Option;
    }
}
using System.CommandLine;
using Animation2Tilemap.Console.CommandLineOptions.Contracts;

namespace Animation2Tilemap.Console.CommandLineOptions;

public class OutputOption : ICommandLineOption<string>
{
    public OutputOption()
    {
        Option = new Option<string>(
            name: "--output",
            description: "Output folder",
            getDefaultValue: () => "output");
        Option.AddAlias("-o");
    }

    public Option<string> Option { get; }

    public Option<string> Register(Command command)
    {
        command.Add(Option);
        command.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(Option);
            string? outputPath;
            try
            {
                outputPath = optionResult?.GetValueOrDefault<string>() ?? string.Empty;
            }
            catch (InvalidOperationException)
            {
                outputPath = string.Empty;
            }

            if (outputPath.Equals(string.Empty))
            {
                return;
            }

            if (outputPath.Any(c => Path.GetInvalidPathChars().Contains(c)))
            {
                result.ErrorMessage = $"The provided output folder '{outputPath}' is invalid.";
                return;
            }

            try
            {
                Directory.CreateDirectory(outputPath);
                using var testFile = File.Create(Path.Combine(outputPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or DirectoryNotFoundException or PathTooLongException)
            {
                result.ErrorMessage = $"The provided output folder '{outputPath}' is not accessible: {ex.Message}";
            }
        });
        return Option;
    }
}
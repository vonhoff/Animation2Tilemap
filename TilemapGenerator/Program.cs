using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;
using System.Text.RegularExpressions;
using TilemapGenerator.Common;

namespace TilemapGenerator;

public static partial class Program
{
    private const string Description = """
            TilemapGenerator is a command-line tool that generates a tileset and a tilemap from images.
            The tool supports animation processing and allows you to set the duration of animation frames.
            """;

    public static async Task Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        var rootCommand = new RootCommand(Description);
        var optionsBinder = BuildApplicationOptions(rootCommand);

        rootCommand.SetHandler(options =>
        {
            var startup = new Startup(options);
            var application = startup.BuildApplication();
            application.Run();
        }, optionsBinder);

        var parser = new CommandLineBuilder(rootCommand)
            .UseHelp("--help", "-?", "/?")
            .UseEnvironmentVariableDirective()
            .UseParseDirective()
            .UseSuggestDirective()
            .RegisterWithDotnetSuggest()
            .UseTypoCorrections()
            .UseParseErrorReporting()
            .UseExceptionHandler()
            .CancelOnProcessTermination()
            .Build();

        await parser.InvokeAsync(args);
    }

    private static ApplicationOptionsBinder BuildApplicationOptions(Command rootCommand)
    {
        #region frameDurationOption

        var frameDurationOption = new Option<int>(
            name: "--duration",
            description: "Animation frame duration",
            getDefaultValue: () => 200);
        frameDurationOption.AddAlias("-d");
        rootCommand.Add(frameDurationOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(frameDurationOption);
            var duration = optionResult?.GetValueOrDefault<int>();
            if (duration is <= 0)
            {
                result.ErrorMessage = $"Invalid frame duration '{duration}'." +
                                      " Animation frame duration should be greater than 0.";
            }
        });

        #endregion frameDurationOption

        #region inputOption

        var inputOption = new Option<string>(
            name: "--input",
            description: "Input file or folder path");
        inputOption.AddAlias("-i");
        inputOption.IsRequired = true;
        rootCommand.Add(inputOption);

        #endregion inputOption

        #region outputOption

        var outputOption = new Option<string>(
            name: "--output",
            description: "Output folder",
            getDefaultValue: () => string.Empty);
        outputOption.AddAlias("-o");
        rootCommand.Add(outputOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(outputOption);
            var outputPath = optionResult?.GetValueOrDefault<string>();
            if (string.IsNullOrEmpty(outputPath))
            {
                return;
            }

            if (File.Exists(outputPath))
            {
                result.ErrorMessage = $"The provided output path '{outputPath}' is not a folder.";
                return;
            }

            if (Directory.Exists(outputPath))
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(outputPath);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or DirectoryNotFoundException or PathTooLongException)
            {
                result.ErrorMessage = $"The provided output folder '{outputPath}' is not accessible: {ex.Message}";
            }
        });

        #endregion outputOption

        #region heightOption

        var heightOption = new Option<int>(
            name: "--height",
            description: "Tile height",
            getDefaultValue: () => 8);
        heightOption.AddAlias("-h");
        rootCommand.Add(heightOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(heightOption);
            var height = optionResult?.GetValueOrDefault<int>();
            if (height is <= 0)
            {
                result.ErrorMessage = $"Invalid height '{height}'." +
                                      " Height should be greater than 0.";
            }
        });

        #endregion heightOption

        #region widthOption

        var widthOption = new Option<int>(
            name: "--width",
            description: "Tile width",
            getDefaultValue: () => 8);
        widthOption.AddAlias("-w");
        rootCommand.Add(widthOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(widthOption);
            var width = optionResult?.GetValueOrDefault<int>();
            if (width is <= 0)
            {
                result.ErrorMessage = $"Invalid width '{width}'." +
                                      " Width should be greater than 0.";
            }
        });

        #endregion widthOption

        #region transparentColorOption

        var transparentColorOption = new Option<string>(
            name: "--transparent",
            description: "Transparent color (RGBA)",
            getDefaultValue: () => "00000000");
        transparentColorOption.AddAlias("-t");
        rootCommand.Add(transparentColorOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(transparentColorOption);
            var transparentColor = optionResult?.GetValueOrDefault<string>();
            if (transparentColor != null && !RgbaColorValidationRegex().IsMatch(transparentColor))
            {
                result.ErrorMessage = $"Invalid transparent color '{transparentColor}'." +
                                      " Transparent color must be a valid RGBA color string.";
            }
        });

        #endregion transparentColorOption

        #region tileLayerFormatOption

        var tileLayerFormatOption = new Option<string>(
            name: "--format",
            description: "Tile layer format",
            getDefaultValue: () => "zlib");
        tileLayerFormatOption.AddAlias("-f");
        tileLayerFormatOption.ArgumentHelpName = "base64|zlib|gzip|csv";
        rootCommand.Add(tileLayerFormatOption);
        rootCommand.AddValidator(result =>
        {
            var optionResult = result.FindResultFor(tileLayerFormatOption);
            var availableOptions = tileLayerFormatOption.ArgumentHelpName!.Split("|");
            var format = optionResult?.GetValueOrDefault<string>()?.ToLowerInvariant();
            if (format == null || !availableOptions.Contains(format))
            {
                result.ErrorMessage = $"Invalid format '{format}'. " +
                                      $"Format must be one of the following options: [{string.Join(", ", availableOptions)}]";
            }
        });

        #endregion tileLayerFormatOption

        #region verboseOption

        var verboseOption = new Option<bool>(
            name: "--verbose",
            description: "Enable verbose logging",
            getDefaultValue: () => false);
        verboseOption.AddAlias("-v");
        rootCommand.Add(verboseOption);

        #endregion verboseOption

        return new ApplicationOptionsBinder(
            frameDurationOption,
            inputOption,
            outputOption,
            heightOption,
            widthOption,
            transparentColorOption,
            tileLayerFormatOption,
            verboseOption);
    }

    [GeneratedRegex("^([0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$")]
    private static partial Regex RgbaColorValidationRegex();
}
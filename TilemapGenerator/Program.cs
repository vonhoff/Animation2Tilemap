using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;
using System.Text.RegularExpressions;
using TilemapGenerator.Common.CommandLine;

namespace TilemapGenerator
{
    public static class Program
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
            var optionsBinder = BuildCommandLineOptions(rootCommand);

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

        private static CommandLineOptionsBinder BuildCommandLineOptions(Command rootCommand)
        {
            var animationOption = new Option<bool>(
                name: "--animation",
                description: "Enable animation processing",
                getDefaultValue: () => false);
            animationOption.AddAlias("-a");
            rootCommand.Add(animationOption);

            var animationFrameDurationOption = new Option<int>(
                name: "--duration",
                description: "Animation frame duration",
                getDefaultValue: () => 200);
            animationFrameDurationOption.AddAlias("-d");
            rootCommand.Add(animationFrameDurationOption);
            rootCommand.AddValidator(result =>
            {
                var optionResult = result.FindResultFor(animationFrameDurationOption);
                var duration = optionResult?.GetValueOrDefault<int>();
                if (duration is <= 0)
                {
                    result.ErrorMessage = $"Invalid frame duration '{duration}'." +
                                          " Animation frame duration should be greater than 0.";
                }
            });

            var inputOption = new Option<string>(
                name: "--input",
                description: "Input file or folder path");
            inputOption.AddAlias("-i");
            inputOption.IsRequired = true;
            rootCommand.Add(inputOption);

            var outputOption = new Option<string>(
                name: "--output",
                description: "Output folder",
                getDefaultValue: () => string.Empty);
            outputOption.AddAlias("-o");
            rootCommand.Add(outputOption);

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
                if (transparentColor != null && !Regex.IsMatch(transparentColor, @"^([0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$"))
                {
                    result.ErrorMessage = $"Invalid transparent color '{transparentColor}'." +
                                          " Transparent color must be a valid RGBA color string.";
                }
            });

            var verboseOption = new Option<bool>(
                name: "--verbose",
                description: "Enable verbose logging",
                getDefaultValue: () => false);
            verboseOption.AddAlias("-v");
            rootCommand.Add(verboseOption);

            return new CommandLineOptionsBinder(
                animationOption,
                animationFrameDurationOption,
                inputOption,
                outputOption,
                heightOption,
                widthOption,
                transparentColorOption,
                verboseOption);
        }
    }
}
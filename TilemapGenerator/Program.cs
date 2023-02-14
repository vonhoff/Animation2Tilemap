using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;
using TilemapGenerator.Common;

namespace TilemapGenerator
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var rootCommand = new RootCommand();

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

            var inputOption = new Option<string>(
                name: "--input",
                description: "Input file or folder path",
                getDefaultValue: () => string.Empty);
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

            var widthOption = new Option<int>(
                name: "--width",
                description: "Tile width",
                getDefaultValue: () => 8);
            widthOption.AddAlias("-w");
            rootCommand.Add(widthOption);

            var transparentColorOption = new Option<string>(
                name: "--transparent",
                description: "Transparent color (RGBA)",
                getDefaultValue: () => "00000000");
            transparentColorOption.AddAlias("-t");
            rootCommand.Add(transparentColorOption);

            var verboseOption = new Option<bool>(
                name: "--verbose",
                description: "Enable verbose logging",
                getDefaultValue: () => false);
            verboseOption.AddAlias("-v");
            rootCommand.Add(verboseOption);

            rootCommand.SetHandler(Application.Run,
                new CommandLineOptionsBinder(
                animationOption,
                animationFrameDurationOption,
                inputOption,
                outputOption,
                heightOption,
                widthOption,
                transparentColorOption,
                verboseOption));

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
    }
}
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;
using Animation2Tilemap.CommandLineOptions;
using Animation2Tilemap.CommandLineOptions.Binding;
using Animation2Tilemap.CommandLineOptions.Contracts;

namespace Animation2Tilemap;

public static class Program
{
    private const string Description =
        """
        Animation2Tilemap is a command-line tool that generates tilesets and tilemaps from animated or non-animated images.
        See: https://github.com/vonhoff/Animation2Tilemap for more information.
        """;

    public static async Task Main(string[] args)
    {
        System.Console.InputEncoding = Encoding.UTF8;
        System.Console.OutputEncoding = Encoding.UTF8;

        var rootCommand = new RootCommand(Description);
        var optionsBinder = BuildMainWorkflowOptionsBinder(rootCommand);

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
            .UseParseErrorReporting()
            .UseExceptionHandler()
            .CancelOnProcessTermination()
            .Build();

        await parser.InvokeAsync(args);
    }

    private static MainWorkflowOptionsBinder BuildMainWorkflowOptionsBinder(Command rootCommand)
    {
        ICommandLineOption<int> frameDurationOption = new FrameDurationOption();
        ICommandLineOption<string> inputOption = new InputOption();
        ICommandLineOption<string> outputOption = new OutputOption();
        ICommandLineOption<int> heightOption = new HeightOption();
        ICommandLineOption<int> widthOption = new WidthOption();
        ICommandLineOption<int> marginOption = new MarginOption();
        ICommandLineOption<int> spacingOption = new SpacingOption();
        ICommandLineOption<string> transparentColorOption = new TransparentColorOption();
        ICommandLineOption<string> tileLayerFormatOption = new TileLayerFormatOption();
        ICommandLineOption<bool> verboseOption = new VerboseOption();

        return new MainWorkflowOptionsBinder(
            rootCommand,
            frameDurationOption,
            inputOption,
            outputOption,
            heightOption,
            widthOption,
            marginOption,
            spacingOption,
            transparentColorOption,
            tileLayerFormatOption,
            verboseOption
        );
    }
}
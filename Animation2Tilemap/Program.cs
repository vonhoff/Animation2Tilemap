using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Text;
using Animation2Tilemap.CommandLineOptions;
using Animation2Tilemap.CommandLineOptions.Binding;

namespace Animation2Tilemap;

public static class Program
{
    private const string Description = """
        Animation2Tilemap is a command-line tool that converts animations into tilesets and tilemaps that can be used in Tiled,
        a popular map editor for 2D games. With Animation2Tilemap, you can easily create tile-based versions of your animations
        and import them into Tiled for further editing.
        """;

    public static async Task Main(string[] args)
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        var rootCommand = new RootCommand(Description);
        var optionsBinder = BuildApplicationOptionsBinder(rootCommand);

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

    private static ApplicationOptionsBinder BuildApplicationOptionsBinder(Command rootCommand)
    {
        var frameDurationOption = new FrameDurationOption();
        var inputOption = new InputOption();
        var outputOption = new OutputOption();
        var heightOption = new HeightOption();
        var widthOption = new WidthOption();
        var transparentColorOption = new TransparentColorOption();
        var tileLayerFormatOption = new TileLayerFormatOption();
        var verboseOption = new VerboseOption();

        return new ApplicationOptionsBinder(
            frameDurationOption.Register(rootCommand),
            inputOption.Register(rootCommand),
            outputOption.Register(rootCommand),
            heightOption.Register(rootCommand),
            widthOption.Register(rootCommand),
            transparentColorOption.Register(rootCommand),
            tileLayerFormatOption.Register(rootCommand),
            verboseOption.Register(rootCommand)
        );
    }
}
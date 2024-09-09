using Animation2Tilemap.Console.CommandLineOptions.Contracts;
using Animation2Tilemap.Core.Enums;
using Animation2Tilemap.Core.Workflows;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.CommandLine;
using System.CommandLine.Binding;

namespace Animation2Tilemap.Console.CommandLineOptions.Binding;

public class MainWorkflowOptionsBinder(
    Command rootCommand,
    ICommandLineOption<int> frameDurationOption,
    ICommandLineOption<string> inputOption,
    ICommandLineOption<string> outputOption,
    ICommandLineOption<int> tileHeightOption,
    ICommandLineOption<int> tileWidthOption,
    ICommandLineOption<int> tileMarginOption,
    ICommandLineOption<int> tileSpacingOption,
    ICommandLineOption<string> transparentColorOption,
    ICommandLineOption<string> tileLayerFormatOption,
    ICommandLineOption<bool> verboseOption)
    : BinderBase<MainWorkflowOptions>
{
    private readonly Option<int> _frameDurationOption = frameDurationOption.Register(rootCommand);
    private readonly Option<string> _inputOption = inputOption.Register(rootCommand);
    private readonly Option<string> _outputOption = outputOption.Register(rootCommand);
    private readonly Option<int> _tileHeightOption = tileHeightOption.Register(rootCommand);
    private readonly Option<string> _tileLayerFormatOption = tileLayerFormatOption.Register(rootCommand);
    private readonly Option<int> _tileMarginOption = tileMarginOption.Register(rootCommand);
    private readonly Option<int> _tileSpacingOption = tileSpacingOption.Register(rootCommand);
    private readonly Option<int> _tileWidthOption = tileWidthOption.Register(rootCommand);
    private readonly Option<string> _transparentColorOption = transparentColorOption.Register(rootCommand);
    private readonly Option<bool> _verboseOption = verboseOption.Register(rootCommand);

    protected override MainWorkflowOptions GetBoundValue(BindingContext bindingContext)
    {
        var parseResult = bindingContext.ParseResult;

        var frameDuration = parseResult.GetValueForOption(_frameDurationOption);
        var input = parseResult.GetValueForOption(_inputOption)!;
        var output = parseResult.GetValueForOption(_outputOption)!;
        var tileWidth = parseResult.GetValueForOption(_tileHeightOption);
        var tileHeight = parseResult.GetValueForOption(_tileWidthOption);
        var tileMargin = parseResult.GetValueForOption(_tileMarginOption);
        var tileSpacing = parseResult.GetValueForOption(_tileSpacingOption);
        var transparentHex = parseResult.GetValueForOption(_transparentColorOption)!;
        var tileLayerFormatString = parseResult.GetValueForOption(_tileLayerFormatOption)!;
        var verbose = parseResult.GetValueForOption(_verboseOption);

        var tileSize = new Size(tileWidth, tileHeight);
        var transparentColor = Rgba32.ParseHex(transparentHex);
        var tileLayerFormat = tileLayerFormatString switch
        {
            "base64" => TileLayerFormat.Base64Uncompressed,
            "zlib" => TileLayerFormat.Base64ZLib,
            "gzip" => TileLayerFormat.Base64GZip,
            "csv" => TileLayerFormat.Csv,
            _ => throw new IndexOutOfRangeException("Invalid tile layer format.")
        };

        return new MainWorkflowOptions
        {
            FrameDuration = frameDuration,
            Input = input,
            Output = output,
            TileSize = tileSize,
            TileMargin = tileMargin,
            TileSpacing = tileSpacing,
            TransparentColor = transparentColor,
            TileLayerFormat = tileLayerFormat,
            Verbose = verbose
        };
    }
}
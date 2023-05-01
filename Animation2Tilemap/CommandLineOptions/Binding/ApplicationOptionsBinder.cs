using System.CommandLine;
using System.CommandLine.Binding;
using Animation2Tilemap.Enums;

namespace Animation2Tilemap.CommandLineOptions.Binding;

public sealed class ApplicationOptionsBinder : BinderBase<ApplicationOptions>
{
    private readonly Option<int> _frameDurationOption;
    private readonly Option<string> _inputOption;
    private readonly Option<string> _outputOption;
    private readonly Option<int> _tileHeightOption;
    private readonly Option<int> _tileWidthOption;
    private readonly Option<string> _transparentColorOption;
    private readonly Option<string> _tileLayerFormatOption;
    private readonly Option<bool> _verboseOption;

    public ApplicationOptionsBinder(
        Option<int> frameDurationOption,
        Option<string> inputOption,
        Option<string> outputOption,
        Option<int> tileHeightOption,
        Option<int> tileWidthOption,
        Option<string> transparentColorOption,
        Option<string> tileLayerFormatOption,
        Option<bool> verboseOption)
    {
        _frameDurationOption = frameDurationOption;
        _inputOption = inputOption;
        _outputOption = outputOption;
        _tileHeightOption = tileHeightOption;
        _tileWidthOption = tileWidthOption;
        _transparentColorOption = transparentColorOption;
        _tileLayerFormatOption = tileLayerFormatOption;
        _verboseOption = verboseOption;
    }

    protected override ApplicationOptions GetBoundValue(BindingContext bindingContext)
    {
        var frameDuration = bindingContext.ParseResult.GetValueForOption(_frameDurationOption);
        var input = bindingContext.ParseResult.GetValueForOption(_inputOption)!;
        var output = bindingContext.ParseResult.GetValueForOption(_outputOption)!;
        var tileWidth = bindingContext.ParseResult.GetValueForOption(_tileHeightOption);
        var tileHeight = bindingContext.ParseResult.GetValueForOption(_tileWidthOption);
        var transparentHex = bindingContext.ParseResult.GetValueForOption(_transparentColorOption)!;
        var tileLayerFormatString = bindingContext.ParseResult.GetValueForOption(_tileLayerFormatOption)!;
        var verbose = bindingContext.ParseResult.GetValueForOption(_verboseOption);

        var tileSize = new Size(tileWidth, tileHeight);
        var transparentColor = Rgba32.ParseHex(transparentHex);
        var tileLayerFormat = tileLayerFormatString switch
        {
            "base64" => TileLayerFormat.Base64Uncompressed,
            "zlib" => TileLayerFormat.Base64ZLib,
            "gzip" => TileLayerFormat.Base64GZip,
            "csv" => TileLayerFormat.CSV,
            _ => throw new IndexOutOfRangeException("Invalid tile layer format.")
        };

        return new ApplicationOptions(
            frameDuration,
            input,
            output,
            tileSize,
            transparentColor,
            tileLayerFormat,
            verbose);
    }
}
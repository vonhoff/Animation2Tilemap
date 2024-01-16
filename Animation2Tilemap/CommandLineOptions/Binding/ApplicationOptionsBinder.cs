using System.CommandLine;
using System.CommandLine.Binding;
using Animation2Tilemap.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.CommandLineOptions.Binding;

public class ApplicationOptionsBinder(
    Option<int> frameDurationOption,
    Option<string> inputOption,
    Option<string> outputOption,
    Option<int> tileHeightOption,
    Option<int> tileWidthOption,
    Option<int> tileMarginOption,
    Option<int> tileSpacingOption,
    Option<string> transparentColorOption,
    Option<string> tileLayerFormatOption,
    Option<bool> verboseOption)
    : BinderBase<ApplicationOptions>
{
    protected override ApplicationOptions GetBoundValue(BindingContext bindingContext)
    {
        var parseResult = bindingContext.ParseResult;

        var frameDuration = parseResult.GetValueForOption(frameDurationOption);
        var input = parseResult.GetValueForOption(inputOption)!;
        var output = parseResult.GetValueForOption(outputOption)!;
        var tileWidth = parseResult.GetValueForOption(tileHeightOption);
        var tileHeight = parseResult.GetValueForOption(tileWidthOption);
        var tileMargin = parseResult.GetValueForOption(tileMarginOption);
        var tileSpacing = parseResult.GetValueForOption(tileSpacingOption);
        var transparentHex = parseResult.GetValueForOption(transparentColorOption)!;
        var tileLayerFormatString = parseResult.GetValueForOption(tileLayerFormatOption)!;
        var verbose = parseResult.GetValueForOption(verboseOption);

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

        return new ApplicationOptions
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
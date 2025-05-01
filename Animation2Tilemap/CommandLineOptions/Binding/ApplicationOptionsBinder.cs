﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.CommandLine;
using System.CommandLine.Binding;
using Animation2Tilemap.CommandLineOptions.Contracts;
using Animation2Tilemap.Enums;
using Animation2Tilemap.Workflows;

namespace Animation2Tilemap.CommandLineOptions.Binding;

public class MainWorkflowOptionsBinder : BinderBase<MainWorkflowOptions>
{
    private readonly Option<int> _frameDurationOption;
    private readonly Option<string> _inputOption;
    private readonly Option<string> _outputOption;
    private readonly Option<int> _tileHeightOption;
    private readonly Option<string> _tileLayerFormatOption;
    private readonly Option<int> _tileMarginOption;
    private readonly Option<int> _tileSpacingOption;
    private readonly Option<int> _tileWidthOption;
    private readonly Option<string> _transparentColorOption;
    private readonly Option<bool> _verboseOption;

    public MainWorkflowOptionsBinder(Command rootCommand,
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
    {
        _frameDurationOption = frameDurationOption.Register(rootCommand);
        _inputOption = inputOption.Register(rootCommand);
        _outputOption = outputOption.Register(rootCommand);
        _tileHeightOption = tileHeightOption.Register(rootCommand);
        _tileLayerFormatOption = tileLayerFormatOption.Register(rootCommand);
        _tileMarginOption = tileMarginOption.Register(rootCommand);
        _tileSpacingOption = tileSpacingOption.Register(rootCommand);
        _tileWidthOption = tileWidthOption.Register(rootCommand);
        _transparentColorOption = transparentColorOption.Register(rootCommand);
        _verboseOption = verboseOption.Register(rootCommand);
    }

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
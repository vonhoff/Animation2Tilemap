using System.CommandLine;
using System.CommandLine.Binding;

namespace TilemapGenerator.CLI;

public class ApplicationOptionsBinder : BinderBase<ApplicationOptions>
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
        return new ApplicationOptions(
            bindingContext.ParseResult.GetValueForOption(_frameDurationOption),
            bindingContext.ParseResult.GetValueForOption(_inputOption)!,
            bindingContext.ParseResult.GetValueForOption(_outputOption)!,
            bindingContext.ParseResult.GetValueForOption(_tileHeightOption),
            bindingContext.ParseResult.GetValueForOption(_tileWidthOption),
            bindingContext.ParseResult.GetValueForOption(_transparentColorOption)!,
            bindingContext.ParseResult.GetValueForOption(_tileLayerFormatOption)!,
            bindingContext.ParseResult.GetValueForOption(_verboseOption)
        );
    }
}
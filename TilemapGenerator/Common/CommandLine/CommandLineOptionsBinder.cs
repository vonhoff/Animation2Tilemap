using System.CommandLine;
using System.CommandLine.Binding;

namespace TilemapGenerator.Common.CommandLine
{
    public class CommandLineOptionsBinder : BinderBase<CommandLineOptions>
    {
        private readonly Option<bool> _animationOption;
        private readonly Option<int> _animationFrameDurationOption;
        private readonly Option<string> _inputOption;
        private readonly Option<string> _outputOption;
        private readonly Option<int> _tileHeightOption;
        private readonly Option<int> _tileWidthOption;
        private readonly Option<string> _transparentColorOption;
        private readonly Option<bool> _verboseOption;

        public CommandLineOptionsBinder(
            Option<bool> animationOption,
            Option<int> animationFrameDurationOption,
            Option<string> inputOption,
            Option<string> outputOption,
            Option<int> tileHeightOption,
            Option<int> tileWidthOption,
            Option<string> transparentColorOption,
            Option<bool> verboseOption)
        {
            _animationOption = animationOption;
            _animationFrameDurationOption = animationFrameDurationOption;
            _inputOption = inputOption;
            _outputOption = outputOption;
            _tileHeightOption = tileHeightOption;
            _tileWidthOption = tileWidthOption;
            _transparentColorOption = transparentColorOption;
            _verboseOption = verboseOption;
        }

        protected override CommandLineOptions GetBoundValue(BindingContext bindingContext)
        {
            return new CommandLineOptions(
                bindingContext.ParseResult.GetValueForOption(_animationOption),
                bindingContext.ParseResult.GetValueForOption(_animationFrameDurationOption),
                bindingContext.ParseResult.GetValueForOption(_inputOption)!,
                bindingContext.ParseResult.GetValueForOption(_outputOption)!,
                bindingContext.ParseResult.GetValueForOption(_tileHeightOption),
                bindingContext.ParseResult.GetValueForOption(_tileWidthOption),
                bindingContext.ParseResult.GetValueForOption(_transparentColorOption)!,
                bindingContext.ParseResult.GetValueForOption(_verboseOption)
            );
        }
    }
}
using System.CommandLine;
using System.CommandLine.Binding;

namespace TilemapGenerator.Common.Configuration
{
    public class ApplicationOptionsBinder : BinderBase<ApplicationOptions>
    {
        private readonly Option<int> _animationFrameDurationOption;
        private readonly Option<string> _inputOption;
        private readonly Option<string> _outputOption;
        private readonly Option<int> _tileHeightOption;
        private readonly Option<int> _tileWidthOption;
        private readonly Option<string> _transparentColorOption;
        private readonly Option<bool> _verboseOption;

        public ApplicationOptionsBinder(
            Option<int> animationFrameDurationOption,
            Option<string> inputOption,
            Option<string> outputOption,
            Option<int> tileHeightOption,
            Option<int> tileWidthOption,
            Option<string> transparentColorOption,
            Option<bool> verboseOption)
        {
            _animationFrameDurationOption = animationFrameDurationOption;
            _inputOption = inputOption;
            _outputOption = outputOption;
            _tileHeightOption = tileHeightOption;
            _tileWidthOption = tileWidthOption;
            _transparentColorOption = transparentColorOption;
            _verboseOption = verboseOption;
        }

        protected override ApplicationOptions GetBoundValue(BindingContext bindingContext)
        {
            return new ApplicationOptions(
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
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Xunit.Abstractions;

namespace TilemapGenerator.Test
{
    public class TestOutputHelperSink : ILogEventSink
    {
        private readonly ITestOutputHelper _output;
        private readonly ITextFormatter _formatter;
        private readonly ITextFormatter _exceptionFormatter;

        public TestOutputHelperSink(ITestOutputHelper output)
        {
            _output = output;
            _formatter = new MessageTemplateTextFormatter("[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}");
            _exceptionFormatter = new MessageTemplateTextFormatter("{Exception}");
        }

        public void Emit(LogEvent logEvent)
        {
            var writer = new StringWriter();
            var exceptionWriter = new StringWriter();

            _formatter.Format(logEvent, writer);
            _exceptionFormatter.Format(logEvent, exceptionWriter);
            _output.WriteLine(writer.ToString());

            var exception = exceptionWriter.ToString();
            if (!string.IsNullOrWhiteSpace(exception))
            {
                _output.WriteLine(exception);
            }
        }
    }
}
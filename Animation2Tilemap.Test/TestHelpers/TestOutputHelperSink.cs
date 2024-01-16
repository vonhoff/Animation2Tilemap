using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Xunit.Abstractions;

namespace Animation2Tilemap.Test.TestHelpers;

public class TestOutputHelperSink(ITestOutputHelper output) : ILogEventSink
{
    private readonly MessageTemplateTextFormatter _formatter = new("[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}");
    private readonly MessageTemplateTextFormatter _exceptionFormatter = new("{Exception}");

    public void Emit(LogEvent logEvent)
    {
        var writer = new StringWriter();
        var exceptionWriter = new StringWriter();

        _formatter.Format(logEvent, writer);
        _exceptionFormatter.Format(logEvent, exceptionWriter);
        output.WriteLine(writer.ToString());

        var exception = exceptionWriter.ToString();
        if (string.IsNullOrWhiteSpace(exception) == false)
        {
            output.WriteLine(exception);
        }
    }
}
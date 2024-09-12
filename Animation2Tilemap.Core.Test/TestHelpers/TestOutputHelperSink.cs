using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Xunit.Abstractions;

namespace Animation2Tilemap.Core.Test.TestHelpers;

public class TestOutputHelperSink : ILogEventSink
{
    private readonly MessageTemplateTextFormatter _exceptionFormatter = new("{Exception}");
    private readonly MessageTemplateTextFormatter _formatter = new("[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}");
    private readonly ITestOutputHelper _output;

    public TestOutputHelperSink(ITestOutputHelper output)
    {
        _output = output;
    }

    public void Emit(LogEvent logEvent)
    {
        var writer = new StringWriter();
        var exceptionWriter = new StringWriter();

        _formatter.Format(logEvent, writer);
        _exceptionFormatter.Format(logEvent, exceptionWriter);
        _output.WriteLine(writer.ToString());

        var exception = exceptionWriter.ToString();
        if (string.IsNullOrWhiteSpace(exception) == false)
        {
            _output.WriteLine(exception);
        }
    }
}
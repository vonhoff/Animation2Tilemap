using System.Diagnostics;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace TilemapGenerator.Logging
{
    public class SerilogCallerEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var skip = 3;
            while (true)
            {
                var stack = new StackFrame(skip);
                if (!stack.HasMethod())
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                    return;
                }

                var method = stack.GetMethod();
                if (method?.DeclaringType != null && method.DeclaringType.Assembly != typeof(Log).Assembly)
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(method.DeclaringType.Name)));
                    return;
                }

                skip++;
            }
        }
    }
}
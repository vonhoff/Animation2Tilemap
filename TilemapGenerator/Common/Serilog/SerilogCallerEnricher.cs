using System.Diagnostics;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace TilemapGenerator.Common.Serilog
{
    public class SerilogCallerEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var logAssembly = typeof(Log).Assembly;

            for (var skip = 3; ; skip++)
            {
                var stack = new StackFrame(skip);
                if (!stack.HasMethod())
                {
                    logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                    return;
                }

                var method = stack.GetMethod();
                if (method?.DeclaringType == null || method.DeclaringType.Assembly == logAssembly)
                {
                    continue;
                }

                var caller = new StringBuilder();
                caller.Append(method.DeclaringType.Name);
                caller.Append('.');
                caller.Append(method.Name);
                logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller.ToString())));
                return;
            }
        }
    }
}
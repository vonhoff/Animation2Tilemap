using Serilog;
using Serilog.Configuration;

namespace TilemapGenerator.Logging;

public static class SerilogCallerEnricherConfiguration
{
    public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration.With(new SerilogCallerEnricher());
    }
}
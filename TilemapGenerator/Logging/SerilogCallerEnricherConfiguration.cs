using Serilog;
using Serilog.Configuration;
using TilemapGenerator.Common;

namespace TilemapGenerator.Logging
{
    public static class SerilogCallerEnricherConfiguration
    {
        public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            return enrichmentConfiguration.With<SerilogCallerEnricher>();
        }
    }
}
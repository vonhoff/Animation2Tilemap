using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace TilemapGenerator.Logging;

public static class ConfigureLogging
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services, bool verbose)
    {
        var logConfig = new LoggerConfiguration();
        string template;

        if (verbose)
        {
            template = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} (at: {Caller}){NewLine}{Exception}";
            logConfig.Enrich.WithCaller();
            logConfig.MinimumLevel.Verbose();
        }
        else
        {
            template = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}";
            logConfig.MinimumLevel.Information();
        }

        logConfig.WriteTo.Console(outputTemplate: template, theme: SerilogConsoleThemes.CustomLiterate);
        Log.Logger = logConfig.CreateLogger();
        services.AddSingleton(Log.Logger);
        return services;
    }
}
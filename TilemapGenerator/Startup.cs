using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TilemapGenerator.Common.Configuration;
using TilemapGenerator.Common.Serilog;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator;

public class Startup
{
    private readonly ApplicationOptions _options;

    public Startup(ApplicationOptions options)
    {
        _options = options;
    }

    public Application BuildApplication()
    {
        var services = new ServiceCollection();
        ConfigureLogging();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<Application>()!;
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_options);
        services.AddSingleton(Log.Logger);

        services.AddSingleton<INamePatternService, NamePatternService>();
        services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
        services.AddSingleton<IImageLoaderService, ImageLoaderService>();
        services.AddSingleton<IImageHashService, ImageHashService>();
        services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
        services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();

        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();

        services.AddSingleton<Application>();
    }

    private void ConfigureLogging()
    {
        var logConfig = new LoggerConfiguration();
        string template;

        if (_options.Verbose)
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
    }
}
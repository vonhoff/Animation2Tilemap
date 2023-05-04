using Animation2Tilemap.Common;
using Animation2Tilemap.Factories;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Animation2Tilemap;

public class Startup
{
    private readonly ApplicationOptions _applicationOptions;

    public Startup(ApplicationOptions applicationOptions)
    {
        _applicationOptions = applicationOptions;
    }

    public Application BuildApplication()
    {
        var services = new ServiceCollection();
        ConfigureLogging(services);
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<Application>();
    }

    private void ConfigureLogging(IServiceCollection services)
    {
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Is(_applicationOptions.Verbose ?
                Serilog.Events.LogEventLevel.Verbose :
                Serilog.Events.LogEventLevel.Information)
            .WriteTo.Console(theme: SerilogConsoleThemes.CustomLiterate);

        Log.Logger = logConfig.CreateLogger();
        services.AddSingleton(Log.Logger);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_applicationOptions);
        services.AddSingleton<INamePatternService, NamePatternService>();
        services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
        services.AddSingleton<IImageLoaderService, ImageLoaderService>();
        services.AddSingleton<IImageHashService, ImageHashService>();
        services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
        services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();
        services.AddSingleton<ITilemapDataService, TilemapDataService>();
        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilemapFactory, TilemapFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();
        services.AddSingleton<Application>();
    }
}
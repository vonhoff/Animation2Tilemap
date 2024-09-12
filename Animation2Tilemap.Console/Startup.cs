using Animation2Tilemap.Console.Common;
using Animation2Tilemap.Console.Services;
using Animation2Tilemap.Core.Factories;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Services;
using Animation2Tilemap.Core.Services.Contracts;
using Animation2Tilemap.Core.Workflows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Animation2Tilemap.Console;

public class Startup
{
    private readonly MainWorkflowOptions _mainWorkflowOptions;

    public Startup(MainWorkflowOptions mainWorkflowOptions)
    {
        _mainWorkflowOptions = mainWorkflowOptions;
    }

    public MainWorkflow BuildApplication()
    {
        var services = new ServiceCollection();
        ConfigureLogging(services);
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<MainWorkflow>();
    }

    private void ConfigureLogging(IServiceCollection services)
    {
        var logConfig = new LoggerConfiguration()
            .MinimumLevel.Is(_mainWorkflowOptions.Verbose ? LogEventLevel.Verbose : LogEventLevel.Information)
            .WriteTo.Console(theme: SerilogConsoleThemes.CustomLiterate);

        Log.Logger = logConfig.CreateLogger();
        services.AddSingleton(Log.Logger);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_mainWorkflowOptions);
        services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();
        services.AddSingleton<INamePatternService, NamePatternService>();
        services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
        services.AddSingleton<IImageLoaderService, ImageLoaderService>();
        services.AddSingleton<IImageHashService, ImageHashService>();
        services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
        services.AddSingleton<ITilemapDataService, TilemapDataService>();
        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilemapFactory, TilemapFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();
        services.AddSingleton<MainWorkflow>();
    }
}
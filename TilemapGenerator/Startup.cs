using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Logging;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator;

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
        ConfigureServices(services);
        ConfigureLogging(services);

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetService<Application>()!;
    }

    private void ConfigureLogging(IServiceCollection services)
    {
        var logConfig = new LoggerConfiguration();
        string template;

        if (_applicationOptions.Verbose)
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
        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();
        services.AddSingleton<Application>();
    }
}
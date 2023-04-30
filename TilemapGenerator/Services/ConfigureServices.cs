using Microsoft.Extensions.DependencyInjection;
using TilemapGenerator.Common;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, ApplicationOptions options)
    {
        services.AddSingleton(options);

        services.AddSingleton<INamePatternService, NamePatternService>();
        services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
        services.AddSingleton<IImageLoaderService, ImageLoaderService>();
        services.AddSingleton<IImageHashService, ImageHashService>();
        services.AddSingleton<IXmlSerializerService, XmlSerializerService>();
        services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();

        services.AddSingleton<ITilesetFactory, TilesetFactory>();
        services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();

        services.AddSingleton<Application>();
        return services;
    }
}
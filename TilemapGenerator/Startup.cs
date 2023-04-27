using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TilemapGenerator.Common.Configuration;
using TilemapGenerator.Common.Serilog;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
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
            ConfigureLogging();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<Application>()!;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_applicationOptions);
            services.AddSingleton(Log.Logger);

            services.AddSingleton<INamePatternService, NamePatternService>();
            services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
            services.AddSingleton<IImageLoaderService, ImageLoaderService>();
            services.AddSingleton<IImageHashService, ImageHashService>();
            services.AddSingleton<ITilesetSerializerService, TilesetSerializerService>();
            services.AddSingleton<IConfirmationDialogService, ConfirmationDialogService>();

            services.AddSingleton<ITilesetFactory, TilesetFactory>();
            services.AddSingleton<ITilesetImageFactory, TilesetImageFactory>();

            services.AddSingleton<Application>();
        }

        private void ConfigureLogging()
        {
            var logConfig = new LoggerConfiguration();

            if (_applicationOptions.Verbose)
            {
                logConfig.MinimumLevel.Verbose();
            }
            else
            {
                logConfig.MinimumLevel.Information();
            }

            logConfig.WriteTo.Console(theme: SerilogConsoleThemes.CustomLiterate);
            Log.Logger = logConfig.CreateLogger();
        }
    }
}
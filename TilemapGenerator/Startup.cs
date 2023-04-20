using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Common;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
    public class Startup
    {
        private readonly CommandLineOptions _options;

        public Startup(CommandLineOptions options)
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
            services.AddSingleton<IAlphanumericPatternService, AlphanumericPatternService>();
            services.AddSingleton<IImageAlignmentService, ImageAlignmentService>();
            services.AddSingleton<IImageLoaderService, ImageLoaderService>();
            services.AddSingleton<ITileHashService, TileHashService>();
            services.AddSingleton<ITilesetTileFactory, TilesetTileFactory>();
            services.AddSingleton<ITilesetFactory, TilesetFactory>();
            services.AddSingleton<Application>();
        }

        private void ConfigureLogging()
        {
            var logConfig = new LoggerConfiguration();

            if (_options.Verbose)
            {
                logConfig.MinimumLevel.Verbose();
            }
            else
            {
                logConfig.MinimumLevel.Information();
            }

            logConfig.WriteTo.Console(theme: CustomConsoleThemes.Literate);
            Log.Logger = logConfig.CreateLogger();
        }
    }
}
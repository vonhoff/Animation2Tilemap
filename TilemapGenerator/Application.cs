using Serilog;
using SixLabors.ImageSharp;
using TilemapGenerator.Common;
using TilemapGenerator.Utilities;

namespace TilemapGenerator
{
    public static class Application
    {
        public static void Run(CommandLineOptions options)
        {
            ConfigureLogging(options.Verbose);
            if (!ImageLoader.TryLoadImages(options.Input, out var images, out var suitableForAnimation))
            {
                return;
            }

            if (!ImageAlignmentUtility.TryAlignImages(images, options.TileSize, options.TransparentColor))
            {
                return;
            }

            // Debug stuff
            Log.Information("Suitable for animation: {Value}", suitableForAnimation);
            foreach (var (filename, frames) in images)
            {
                if (!Directory.Exists("output/" + filename))
                {
                    Directory.CreateDirectory("output/" + filename);
                }

                for (var i = 0; i < frames.Count; i++)
                {
                    var newFilename = Path.Combine("output/" + filename, $"{i:D4}{Path.GetExtension(filename)}");
                    frames[i].Save(newFilename);
                }
            }
        }

        private static void ConfigureLogging(bool verbose)
        {
            var logConfig = new LoggerConfiguration();

            if (verbose)
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
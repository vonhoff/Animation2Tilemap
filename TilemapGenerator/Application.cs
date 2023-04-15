using Serilog;
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

            foreach (var (filename, frames) in images)
            {
                for (var i = 0; i < frames.Count; i++)
                {
                    frames[i] = ImageAlignmentUtility.AlignFrame(frames[i], options.TileSize, options.TransparentColor);
                }

                Log.Information("Aligned {FrameCount} frames for {FileName}",
                    frames.Count, filename);
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
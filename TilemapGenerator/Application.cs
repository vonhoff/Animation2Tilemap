using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Common;
using TilemapGenerator.Utilities;

namespace TilemapGenerator
{
    public static class Application
    {
        public static void Run(CommandLineOptions options)
        {
            ConfigureLogging(options.Verbose);

            if (!CollectImageEntities(options, out var images, out var suitableForAnimation))
            {
                return;
            }



            foreach (var (filename, frames) in images)
            {
                var tileset = TilesetUtility.FromFrames(filename, frames);
            }
        }

        private static bool CollectImageEntities(CommandLineOptions options, 
            out Dictionary<string, List<Image<Rgba32>>> images,
            out bool suitableForAnimation)
        {
            if (!ImageLoader.TryLoadImages(options.Input, out images, out suitableForAnimation))
            {
                return false;
            }

            foreach (var (filename, frames) in images)
            {
                for (var i = 0; i < frames.Count; i++)
                {
                    frames[i] = ImageAlignmentUtility.AlignFrame(frames[i], options.TileSize, options.TransparentColor);
                }

                Log.Information("Aligned {FrameCount} frames for {FileName}", frames.Count, filename);
            }

            return true;
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
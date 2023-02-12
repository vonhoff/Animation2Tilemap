using System.Diagnostics;
using Serilog;
using Serilog.Debugging;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Common;
using TilemapGenerator.Utilities;

namespace TilemapGenerator
{
    public static class Application
    {
        public static void Run(CommandLineOptions options)
        {
            ConfigureLogging(options.Verbose);
            if (!ImageUtility.TryLoadImages(options.Input, out var images, out var canBeAnimated))
            {
                return;
            }

            var color = Rgba32.ParseHex(options.TransparentColor);
            if (!ImageUtility.TryApplyPadding(images, options.TileWidth, options.TileHeight, color))
            {
                return;
            }

            if (options.Animation && !canBeAnimated)
            {
                Log.Error("Could not process the collection as an animation, " +
                            "each image should be of the same size and should contain only a single frame.");
            }

            // Create tileset and tilemap.
        }

        private static void ConfigureLogging(bool verbose)
        {
            SelfLog.Enable(message => Trace.WriteLine($"INTERNAL ERROR: {message}"));
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
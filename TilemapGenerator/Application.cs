using System.Diagnostics;
using Serilog;
using Serilog.Debugging;
using SixLabors.ImageSharp.PixelFormats;
using TilemapGenerator.Common;
using TilemapGenerator.Utilities;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly CommandLineOptions _options;

        public Application(CommandLineOptions options)
        {
            _options = options;
        }

        public void Run()
        {
            ConfigureLogging(_options.Verbose);
            if (!ImageUtility.TryLoadImages(_options.Input, out var images, out var canBeAnimated))
            {
                return;
            }

            var color = Rgba32.ParseHex(_options.TransparentColor);
            if (!ImageUtility.TryApplyPadding(images, _options.TileWidth, _options.TileHeight, color))
            {
                return;
            }

            if (_options.Animation && !canBeAnimated)
            {
                Log.Error("In order to process the collection as a single animation, " +
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
using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Common;
using TilemapGenerator.Utilities;

namespace TilemapGenerator
{
    public static class Application
    {
        /// <summary>
        /// Runs the application with the specified command-line options.
        /// </summary>
        /// <param name="options">The command-line options.</param>
        public static void Run(CommandLineOptions options)
        {
            ConfigureLogging(options.Verbose);

            if (!LoadAndAlignImages(options, out var images))
            {
                return;
            }


        }

        /// <summary>
        /// Configures the Serilog logging framework based on the specified verbosity level.
        /// </summary>
        /// <param name="verbose">A value indicating whether verbose logging should be enabled.</param>
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

        /// <summary>
        /// Loads and aligns images based on the specified command-line options.
        /// </summary>
        /// <param name="options">The command-line options.</param>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        /// <returns>A value indicating whether the loading and alignment succeeded.</returns>
        private static bool LoadAndAlignImages(CommandLineOptions options, out Dictionary<string, List<Image<Rgba32>>> images)
        {
            if (!ImageLoader.TryLoadImages(options.Input, out images, out var suitableForAnimation))
            {
                return false;
            }

            if (suitableForAnimation)
            {
                Log.Information("The loaded image files can be used as animation frames.");

                if (options.Animation)
                {
                    TransformImagesToAnimation(ref images);
                }
                else
                {
                    Log.Warning("Animation processing is disabled. Images will be processed individually.");
                }
            }
            else
            {
                Log.Warning("The loaded image files cannot be used as animation frames. " +
                            "Images will be processed individually.");
            }

            foreach (var (filename, frames) in images)
            {
                for (var i = 0; i < frames.Count; i++)
                {
                    frames[i] = ImageAlignmentUtility.AlignFrame(frames[i], options.TileSize, options.TransparentColor);
                }

                Log.Information("Aligned {FrameCount} frame(s) for {FileName}", frames.Count, filename);
            }

            return true;
        }

        /// <summary>
        /// Groups the loaded and aligned images into a single animation.
        /// </summary>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        private static void TransformImagesToAnimation(ref Dictionary<string, List<Image<Rgba32>>> images)
        {
            Log.Information("Animation processing is enabled. Images are treated as animation frames.");

            List<string> fileNames = images.Keys.Select(Path.GetFileNameWithoutExtension).ToList()!;
            var name = AlphanumericPatternUtility.GetMostOccurringPattern(fileNames);
            var frames = images.Values.Select(v => v.First()).ToList();
            images = new Dictionary<string, List<Image<Rgba32>>>
            {
                { name, frames }
            };
        }
    }
}
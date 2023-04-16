using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Contracts;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly IAlphanumericPatternService _alphanumericPatternService;
        private readonly IImageLoaderService _imageLoaderService;
        private readonly IImageAlignmentService _imageAlignmentService;
        private readonly ILogger _logger;

        public Application(
            IAlphanumericPatternService alphanumericPatternService,
            IImageLoaderService imageLoaderService,
            IImageAlignmentService imageAlignmentService,
            ILogger logger)
        {
            _alphanumericPatternService = alphanumericPatternService;
            _imageLoaderService = imageLoaderService;
            _imageAlignmentService = imageAlignmentService;
            _logger = logger;
        }

        /// <summary>
        /// Runs the application with the specified command-line options.
        /// </summary>
        /// <param name="options">The command-line options.</param>
        public void Run(CommandLineOptions options)
        {
            if (!LoadAndAlignImages(options, out var images))
            {
                return;
            }
        }

        /// <summary>
        /// Loads and aligns images based on the specified command-line options.
        /// </summary>
        /// <param name="options">The command-line options.</param>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        /// <returns>A value indicating whether the loading and alignment succeeded.</returns>
        public bool LoadAndAlignImages(CommandLineOptions options, out Dictionary<string, List<Image<Rgba32>>> images)
        {
            if (!_imageLoaderService.TryLoadImages(options.Input, out images, out var suitableForAnimation))
            {
                return false;
            }

            if (suitableForAnimation)
            {
                _logger.Information("The loaded image files can be used as animation frames.");

                if (options.Animation)
                {
                    TransformImagesToAnimation(ref images);
                }
                else
                {
                    _logger.Warning("Animation processing is disabled. Images will be processed individually.");
                }
            }
            else
            {
                _logger.Warning("The loaded image files cannot be used as animation frames. " +
                                "Images will be processed individually.");
            }

            foreach (var (filename, frames) in images)
            {
                for (var i = 0; i < frames.Count; i++)
                {
                    frames[i] = _imageAlignmentService.AlignFrame(frames[i], options.TileSize, options.TransparentColor);
                }

                _logger.Information("Aligned {FrameCount} frame(s) for {FileName}", frames.Count, filename);
            }

            return true;
        }

        /// <summary>
        /// Groups the loaded and aligned images into a single animation.
        /// </summary>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        public void TransformImagesToAnimation(ref Dictionary<string, List<Image<Rgba32>>> images)
        {
            _logger.Information("Animation processing is enabled. Images are treated as animation frames.");

            List<string> fileNames = images.Keys.Select(Path.GetFileNameWithoutExtension).ToList()!;

            var name = _alphanumericPatternService.GetMostOccurringPattern(fileNames);
            name ??= _alphanumericPatternService.GetMostOccurringLetter(fileNames);
            name ??= "Animation";

            var frames = images.Values.Select(v => v.First()).ToList();
            images = new Dictionary<string, List<Image<Rgba32>>>
            {
                { name, frames }
            };
        }
    }
}
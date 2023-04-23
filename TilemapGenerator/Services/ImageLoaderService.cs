using System.Diagnostics;
using Serilog;
using TilemapGenerator.Common;
using TilemapGenerator.Common.CommandLine;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class ImageLoaderService : IImageLoaderService
    {
        private readonly ILogger _logger;
        private readonly INamePatternService _namePatternService;
        private readonly string _path;
        private readonly bool _requestAnimation;

        public ImageLoaderService(
            ILogger logger, 
            INamePatternService namePatternService,
            CommandLineOptions options)
        {
            _namePatternService = namePatternService;
            _logger = logger;
            _path = options.Input;
            _requestAnimation = options.Animation;
        }

        /// <summary>
        /// Attempts to load images from the specified path and returns a dictionary of image frames keyed by file name.
        /// </summary>
        /// <param name="images">Output parameter that contains the loaded images, if the method succeeds.</param>
        /// <returns><see langword="true"/> if images were loaded successfully, otherwise <see langword="false"/>.</returns>
        public bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images)
        {
            images = new Dictionary<string, List<Image<Rgba32>>>();

            if (!Directory.Exists(_path) && !File.Exists(_path))
            {
                _logger.Error("The input path is invalid.");
                return false;
            }

            if (File.Exists(_path))
            {
                var frames = LoadFromFile(_path);
                if (frames.Any())
                {
                    images.Add(Path.GetFileName(_path), frames);
                }
            }
            else
            {
                images = LoadFromDirectory(_path, out var suitableForAnimation);

                if (suitableForAnimation)
                {
                    _logger.Information("The loaded image files can be used as animation frames.");

                    if (_requestAnimation)
                    {
                        _logger.Information("Animation processing is requested. Images are treated as animation frames.");
                        TransformImagesToAnimation(ref images);
                    }
                    else
                    {
                        _logger.Warning("Animation processing is not requested. Images will be processed individually.");
                    }
                }
                else
                {
                    _logger.Warning("The loaded image files cannot be used as animation frames.");
                }
            }

            if (images.Count == 0)
            {
                _logger.Error("The input path does not lead to any valid images.");
                return false;
            }

            return true;
        }

        private void TransformImagesToAnimation(ref Dictionary<string, List<Image<Rgba32>>> images)
        {
            List<string> fileNames = images.Keys.Select(Path.GetFileNameWithoutExtension).ToList()!;

            var name = _namePatternService.GetMostOccurringPattern(fileNames);
            if (name == null)
            {
                var fileName = "Animation_" + DateTime.Now.ToLocalTime();
                name = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
            }

            _logger.Information("Using {Name} as the animation name.", name);
            var frames = images.Values.Select(v => v.First()).ToList();
            images = new Dictionary<string, List<Image<Rgba32>>>
            {
                { name, frames }
            };
        }

        /// <summary>
        /// Loads all images and their frames from the specified directory.
        /// </summary>
        /// <remarks>
        /// The method will only load images with supported formats. Unsupported formats will be skipped.<br/>
        /// The method will also detect whether the images in the directory can be used as animation frames, and sets the <paramref name="suitableForAnimation"/> parameter accordingly.
        /// </remarks>
        /// <param name="path">The directory path to load images from.</param>
        /// <param name="suitableForAnimation"><see langword="true"/> if all images have the same size and contain a single frame, otherwise <see langword="false"/>.</param>
        /// <returns>A dictionary of image frames keyed by file name.</returns>
        public Dictionary<string, List<Image<Rgba32>>> LoadFromDirectory(string path, out bool suitableForAnimation)
        {
            var images = new Dictionary<string, List<Image<Rgba32>>>();
            var stopwatch = Stopwatch.StartNew();
            var files = Directory
                .GetFiles(path, "*.*")
                .OrderBy(p => p, new NaturalStringComparer())
                .ToList();

            stopwatch.Stop();
            _logger.Information("Found {Count} file(s) in {Directory}. Took: {Elapsed}ms",
                files.Count, path, stopwatch.ElapsedMilliseconds);

            var totalFrames = 0;
            stopwatch.Restart();

            Image? previous = null;
            suitableForAnimation = true;
            foreach (var file in files)
            {
                var frames = LoadFromFile(file);
                if (!frames.Any())
                {
                    continue;
                }

                images.Add(Path.GetFileName(file), frames);
                totalFrames += frames.Count;

                if (suitableForAnimation)
                {
                    var current = frames[0];
                    if (previous != null && !previous.Size.Equals(current.Size) || frames.Count > 1)
                    {
                        suitableForAnimation = false;
                    }

                    previous = frames[0];
                }
            }

            _logger.Information("Loaded {ImageCount} of {InputCount} file(s) containing a total of {FrameCount} frame(s). Took: {Elapsed}ms",
                images.Count, files.Count, totalFrames, stopwatch.ElapsedMilliseconds);
            return images;
        }

        /// <summary>
        /// Loads all frames of an image from a file.
        /// </summary>
        /// <remarks>
        /// If the format of the image is unsupported, an empty list is returned.<br/>
        /// If an exception occurs while loading the image, an empty list is returned and the exception is logged.
        /// </remarks>
        /// <param name="file">The path of the file to load the image from.</param>
        /// <returns>A list of image frames loaded from the file.</returns>
        public List<Image<Rgba32>> LoadFromFile(string file)
        {
            var frames = new List<Image<Rgba32>>();

            try
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                Image.DetectFormat(stream);

                stream.Position = 0;
                var image = Image.Load(stream);
                frames = new List<Image<Rgba32>>();
                for (var i = 0; i < image.Frames.Count; i++)
                {
                    var frame = image.Frames.CloneFrame(i);
                    frames.Add(frame.CloneAs<Rgba32>());
                }

                _logger.Verbose("Loaded {FrameCount} frame(s) from {Path}", image.Frames.Count, file);
                return frames;
            }
            catch (UnknownImageFormatException)
            {
                _logger.Warning("Unsupported format: {Path}", file);
                return frames;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error loading image: {Path}", file);
                return frames;
            }
        }
    }
}
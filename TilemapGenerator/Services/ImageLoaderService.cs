using System.Diagnostics;
using Serilog;
using TilemapGenerator.Common;
using TilemapGenerator.Common.Configuration;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public class ImageLoaderService : IImageLoaderService
{
    private readonly IConfirmationDialogService _confirmationDialogService;
    private readonly ILogger _logger;
    private readonly INamePatternService _namePatternService;
    private readonly string _inputPath;

    public ImageLoaderService(
        ILogger logger,
        INamePatternService namePatternService,
        IConfirmationDialogService confirmationDialogService,
        ApplicationOptions options)
    {
        _namePatternService = namePatternService;
        _confirmationDialogService = confirmationDialogService;
        _logger = logger;
        _inputPath = options.Input;
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
    private Dictionary<string, List<Image<Rgba32>>> LoadFromDirectory(string path, out bool suitableForAnimation)
    {
        var images = new Dictionary<string, List<Image<Rgba32>>>();
        var stopwatch = Stopwatch.StartNew();
        var files = Directory
            .GetFiles(path, "*.*")
            .OrderBy(p => p, new NaturalStringComparer())
            .ToList();

        stopwatch.Stop();
        _logger.Verbose("Found {Count} file(s) in {Directory}. Took: {Elapsed}ms",
            files.Count, path, stopwatch.ElapsedMilliseconds);

        var totalFrames = 0;
        stopwatch.Restart();

        Image? previous = null;
        suitableForAnimation = files.Count > 1;
        foreach (var file in files)
        {
            var frames = LoadFromFile(file);
            if (!frames.Any())
            {
                continue;
            }

            images.Add(Path.GetFileName(file), frames);
            totalFrames += frames.Count;

            if (!suitableForAnimation)
            {
                continue;
            }
            
            var current = frames[0];
            if (previous != null && !previous.Size.Equals(current.Size) || frames.Count > 1)
            {
                suitableForAnimation = false;
            }

            previous = frames[0];
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
    private List<Image<Rgba32>> LoadFromFile(string file)
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

            _logger.Information("Loaded {FrameCount} frame(s) from {Path}", image.Frames.Count, file);
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

    /// <summary>
    /// Attempts to load images from the specified path and returns a dictionary of image frames keyed by file name.
    /// </summary>
    /// <param name="images">Output parameter that contains the loaded images, if the method succeeds.</param>
    /// <returns><see langword="true"/> if images were loaded successfully, otherwise <see langword="false"/>.</returns>
    public bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images)
    {
        images = new Dictionary<string, List<Image<Rgba32>>>();

        if (!Directory.Exists(_inputPath) && !File.Exists(_inputPath))
        {
            _logger.Error("The input path is invalid.");
            return false;
        }

        if (File.Exists(_inputPath))
        {
            var frames = LoadFromFile(_inputPath);
            if (frames.Any())
            {
                images.Add(Path.GetFileName(_inputPath), frames);
            }
        }
        else
        {
            images = LoadFromDirectory(_inputPath, out var suitableForAnimation);

            if (suitableForAnimation)
            {
                var requestAnimation = _confirmationDialogService.Confirm("The loaded images can be processed as animation frames. \n" +
                                                                          "Do you want to create an animation from these images?", true);
                if (requestAnimation)
                {
                    _logger.Information("The loaded images will be processed as animation frames.");
                    TransformImagesToAnimation(ref images);
                }
                else
                {
                    _logger.Information("The loaded images will be processed individually.");
                }
            }
            else
            {
                _logger.Warning("The loaded images cannot be used as animation frames.");
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

        var name = _namePatternService.GetMostNotablePattern(fileNames);
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
}
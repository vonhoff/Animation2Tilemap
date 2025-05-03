using System.Diagnostics;
using Animation2Tilemap.Common;
using Animation2Tilemap.Services.Contracts;
using Animation2Tilemap.Workflows;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Services;

public class ImageLoaderService : IImageLoaderService
{
    private readonly bool _assumeAnimation;
    private readonly IConfirmationDialogService _confirmationDialogService;
    private readonly string _inputPath;
    private readonly ILogger _logger;
    private readonly INamePatternService _namePatternService;

    public ImageLoaderService(
        ILogger logger,
        INamePatternService namePatternService,
        IConfirmationDialogService confirmationDialogService,
        MainWorkflowOptions options)
    {
        _namePatternService = namePatternService;
        _confirmationDialogService = confirmationDialogService;
        _logger = logger;
        _inputPath = options.Input;
        _assumeAnimation = options.AssumeAnimation;
    }

    public bool TryLoadImages(out Dictionary<string, List<Image<Rgba32>>> images)
    {
        images = [];

        if (Directory.Exists(_inputPath) == false && File.Exists(_inputPath) == false)
        {
            _logger.Error("The input path is invalid.");
            return false;
        }

        if (File.Exists(_inputPath))
        {
            var frames = LoadFromFile(_inputPath);
            if (frames.Count != 0)
            {
                images.Add(Path.GetFileName(_inputPath), frames);
            }
        }
        else
        {
            images = LoadFromDirectory(_inputPath, out var suitableForAnimation);

            if (suitableForAnimation)
            {
                bool requestAnimation;

                if (_assumeAnimation)
                {
                    requestAnimation = true;
                    _logger.Information("The loaded images will be processed as animation frames (--assume-animation is set).");
                }
                else
                {
                    requestAnimation = _confirmationDialogService.Confirm("The loaded images can be processed as animation frames. \n" +
                                                                          "Do you want to create an animation from these images?", true);
                    if (requestAnimation)
                    {
                        _logger.Information("The loaded images will be processed as animation frames.");
                    }
                    else
                    {
                        _logger.Information("The loaded images will be processed individually.");
                    }
                }

                if (requestAnimation)
                {
                    TransformImagesToAnimation(ref images);
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
            if (frames.Count == 0)
            {
                continue;
            }

            if (images.TryAdd(Path.GetFileNameWithoutExtension(file), frames) == false)
            {
                images.Add(Path.GetFileName(file), frames);
            }
            totalFrames += frames.Count;

            if (suitableForAnimation == false)
            {
                continue;
            }

            var current = frames[0];
            if (previous != null && previous.Size.Equals(current.Size) == false || frames.Count > 1)
            {
                suitableForAnimation = false;
            }

            previous = frames[0];
        }

        _logger.Information("Loaded {ImageCount} of {InputCount} file(s) containing a total of {FrameCount} frame(s). Took: {Elapsed}ms",
            images.Count, files.Count, totalFrames, stopwatch.ElapsedMilliseconds);
        return images;
    }

    private List<Image<Rgba32>> LoadFromFile(string file)
    {
        var frames = new List<Image<Rgba32>>();

        try
        {
            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            Image.DetectFormat(stream);

            stream.Position = 0;
            var image = Image.Load(stream);
            frames = [];
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
            {
                name, frames
            }
        };
    }
}
using System.Diagnostics;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TilemapGenerator.Common;

namespace TilemapGenerator.Utilities
{
    public static class ImageUtility
    {
        public static bool TryApplyPadding(Dictionary<string, List<Image>> images, int tileWidth, int tileHeight, Rgba32 paddingColor)
        {
            if (images.Count == 0 || tileWidth < 1 || tileHeight < 1)
            {
                return false;
            }

            var paddingApplied = 0;
            var totalStopwatch = Stopwatch.StartNew();

            foreach (var (fileName, frames) in images)
            {
                var initialFrame = frames[0];
                var newWidth = (int)Math.Ceiling((double)initialFrame.Width / tileWidth) * tileWidth;
                var newHeight = (int)Math.Ceiling((double)initialFrame.Height / tileHeight) * tileHeight;

                if (newWidth == initialFrame.Width && newHeight == initialFrame.Height)
                {
                    Log.Verbose("No padding needed for {File} ({Width}x{Height})",
                        fileName, initialFrame.Width, initialFrame.Height);
                    continue;
                }

                for (var i = 0; i < frames.Count; i++)
                {
                    Log.Verbose("Padding {File} [Frame {Frame}] ({Width}x{Height}) to ({PaddedWidth}x{PaddedHeight})",
                        fileName, i, initialFrame.Width, initialFrame.Height, newWidth, newHeight);
                    var paddedImage = new Image<Rgba32>(newWidth, newHeight);
                    paddedImage.Mutate(x => x.BackgroundColor(paddingColor));
                    paddedImage.Mutate(x => x.DrawImage(initialFrame, Point.Empty, 1f));
                    frames[i] = paddedImage;
                }

                paddingApplied++;
            }

            totalStopwatch.Stop();
            Log.Information("Applied padding to {PaddingCount} of {InputCount} image(s). Took: {Elapsed}ms",
                paddingApplied, images.Count, totalStopwatch.ElapsedMilliseconds);
            return true;
        }

        public static bool TryLoadImages(string path, out Dictionary<string, List<Image>> images, out bool canBeAnimated)
        {
            canBeAnimated = true;
            images = new Dictionary<string, List<Image>>();

            if (File.Exists(path))
            {
                Log.Verbose("Input path leads to a file.");
                var stopwatch = Stopwatch.StartNew();
                if (TryLoadImageFrames(path, out var frames))
                {
                    if (frames.Count > 1)
                    {
                        canBeAnimated = false;
                    }

                    images.Add(Path.GetFileName(path), frames);
                    stopwatch.Stop();
                    Log.Information("Loaded {FrameCount} frame(s). Took: {Elapsed}ms",
                        frames.Count, stopwatch.ElapsedMilliseconds);
                }
            }
            else if (Directory.Exists(path))
            {
                Log.Verbose("Input path leads to a directory.");

                var stopwatch = Stopwatch.StartNew();
                var files = Directory
                    .GetFiles(path, "*.*")
                    .OrderBy(p => p, new NaturalStringComparer())
                    .ToList();

                stopwatch.Stop();
                Log.Information("Collected {Count} file(s). Took: {Elapsed}ms",
                    files.Count, stopwatch.ElapsedMilliseconds);

                var totalFrames = 0;
                stopwatch.Restart();

                Image? previous = null;
                foreach (var file in files)
                {
                    if (!TryLoadImageFrames(file, out var frames))
                    {
                        continue;
                    }

                    images.Add(Path.GetFileName(file), frames);
                    totalFrames += frames.Count;

                    if (canBeAnimated)
                    {
                        var current = frames[0];
                        if ((previous != null && !previous.Size().Equals(current.Size())) || frames.Count > 1)
                        {
                            canBeAnimated = false;
                        }
                        previous = frames[0];
                    }
                }

                stopwatch.Stop();
                Log.Information("Loaded {ImageCount} of {InputCount} file(s) containing a total of {FrameCount} frame(s). Took: {Elapsed}ms",
                    images.Count, files.Count, totalFrames, stopwatch.ElapsedMilliseconds);
            }
            else
            {
                Log.Fatal("{Path} is not a valid file or directory path.", path);
                return false;
            }

            if (!images.Any())
            {
                Log.Fatal("{Path} does not contain any valid images.", path);
                return false;
            }

            return true;
        }

        private static bool TryLoadImageFrames(string file, out List<Image> frames)
        {
            try
            {
                using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                var format = Image.DetectFormat(stream);
                if (format == null)
                {
                    Log.Warning("Unsupported format: {Path}", file);
                    frames = new List<Image>();
                    return false;
                }

                stream.Position = 0;
                var image = Image.Load(stream);

                frames = new List<Image>();
                for (var i = 0; i < image.Frames.Count; i++)
                {
                    var frame = image.Frames.CloneFrame(i);
                    frames.Add(frame);
                }

                Log.Verbose("Loaded {FrameCount} frame(s) from: {Path}", image.Frames.Count, file);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error loading image: {Path}", file);
                frames = new List<Image>();
                return false;
            }
        }
    }
}
using System.Diagnostics;
using Serilog;
using TilemapGenerator.Common;

namespace TilemapGenerator.Utilities
{
    public static class ImageLoader
    {
        /// <summary>
        /// Attempts to load images from the specified path and returns a dictionary of image frames keyed by file name.
        /// </summary>
        /// <remarks>
        /// If <paramref name="path"/> points to a file, the method will load that file as a single image with its frames.<br/>
        /// If <paramref name="path"/> points to a directory, the method will load all supported images in that directory and return a dictionary of image frames keyed by file name.
        /// </remarks>
        /// <param name="path">The path of the file or directory to load images from.</param>
        /// <param name="images">Output parameter that contains the loaded images, if the method succeeds.</param>
        /// <param name="suitableForAnimation">Output parameter that indicates whether or not the loaded images are suitable for use as animation frames.</param>
        /// <returns><see langword="true"/> if images were loaded successfully, otherwise <see langword="false"/>.</returns>
        public static bool TryLoadImages(string path, out Dictionary<string, List<Image<Rgba32>>> images, out bool suitableForAnimation)
        {
            images = new Dictionary<string, List<Image<Rgba32>>>();
            suitableForAnimation = false;

            if (!Directory.Exists(path) && !File.Exists(path))
            {
                Log.Error("The input path is invalid.");
                return false;
            }

            if (File.Exists(path))
            {
                var frames = LoadFromFile(path);
                if (frames.Any())
                {
                    images.Add(Path.GetFileName(path), frames);
                }
            }
            else
            {
                images = LoadFromDirectory(path, out suitableForAnimation);
            }

            if (images.Count == 0)
            {
                suitableForAnimation = false;
                Log.Error("The input path does not lead to any valid images.");
                return false;
            }

            return true;
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
        public static Dictionary<string, List<Image<Rgba32>>> LoadFromDirectory(string path, out bool suitableForAnimation)
        {
            var images = new Dictionary<string, List<Image<Rgba32>>>();
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
                    if ((previous != null && !previous.Size.Equals(current.Size)) || frames.Count > 1)
                    {
                        suitableForAnimation = false;
                    }

                    previous = frames[0];
                }
            }

            stopwatch.Stop();
            Log.Information("Loaded {ImageCount} of {InputCount} file(s) containing a total of {FrameCount} frame(s). Took: {Elapsed}ms.",
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
        public static List<Image<Rgba32>> LoadFromFile(string file)
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
                    frames.Add((Image<Rgba32>)frame);
                }

                Log.Verbose("Loaded {FrameCount} frame(s) from: {Path}", image.Frames.Count, file);
                return frames;
            }
            catch (UnknownImageFormatException)
            {
                Log.Warning("Unsupported format: {Path}", file);
                return frames;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error loading image: {Path}", file);
                return frames;
            }
        }
    }
}
using System.Diagnostics;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TilemapGenerator.Utilities
{
    public static class ImageAlignmentUtility
    {
        /// <summary>
        /// Attempts to align a collection of images to a grid of tiles.
        /// </summary>
        /// <remarks>
        /// Each image in the <paramref name="images"/> dictionary is aligned to the grid individually.
        /// </remarks>
        /// <param name="images">A dictionary of image frames keyed by file name.</param>
        /// <param name="tileSize">The size of the tiles to align the images to.</param>
        /// <param name="backgroundColor">The color of the background to use for padding.</param>
        /// <returns><see langword="true"/> if any images were aligned, <see langword="false"/> otherwise.</returns>
        public static bool TryAlignImages(Dictionary<string, List<Image<Rgba32>>> images, Size tileSize, Rgba32 backgroundColor)
        {
            if (!images.Any())
            {
                Log.Error("The input images dictionary is empty.");
                return false;
            }

            var alignmentApplied = 0;
            var totalStopwatch = Stopwatch.StartNew();
            foreach (var (fileName, frames) in images)
            {
                if (TryApplyAlignment(frames, tileSize, backgroundColor, fileName))
                {
                    alignmentApplied++;
                }
            }

            totalStopwatch.Stop();
            Log.Information("Aligned {PaddingCount} of {InputCount} image(s). Took: {Elapsed}ms.",
                alignmentApplied, images.Count, totalStopwatch.ElapsedMilliseconds);
            return true;
        }

        /// <summary>
        /// Attempts to align a collection of frames to a grid of tiles.
        /// </summary>
        /// <param name="frames">A collection of frames to align.</param>
        /// <param name="tileSize">The size of the tiles to align the frames to.</param>
        /// <param name="backgroundColor">The color of the background to use.</param>
        /// <param name="fileName">The name of the file that the frames belong to.</param>
        /// <returns><see langword="true"/> if the frames were aligned, <see langword="false"/> otherwise.</returns>
        public static bool TryApplyAlignment(List<Image<Rgba32>> frames, Size tileSize, Rgba32 backgroundColor, string fileName)
        {
            if (!frames.Any())
            {
                Log.Warning("The frames collection for {File} is empty.", fileName);
                return false;
            }

            var initialFrame = frames[0];
            var alignedWidth = (int)Math.Ceiling((double)initialFrame.Width / tileSize.Width) * tileSize.Width;
            var alignedHeight = (int)Math.Ceiling((double)initialFrame.Height / tileSize.Height) * tileSize.Height;

            for (var i = 0; i < frames.Count; i++)
            {
                var frame = frames[i];
                var alignedFrame = new Image<Rgba32>(alignedWidth, alignedHeight);
                alignedFrame.Mutate(context => context.BackgroundColor(backgroundColor));
                alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));
                frames[i] = alignedFrame;
            }

            if (alignedWidth == initialFrame.Width && alignedHeight == initialFrame.Height)
            {
                Log.Verbose("No alignment applied to {File} ({Width}x{Height}).",
                    fileName, initialFrame.Width, initialFrame.Height);
                return false;
            }

            Log.Verbose("Aligned {File} ({Width}x{Height}) to ({AlignedWidth}x{AlignedHeight}).",
                fileName, initialFrame.Width, initialFrame.Height, alignedWidth, alignedHeight);
            return true;
        }
    }
}
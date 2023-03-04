﻿using System.Diagnostics;
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

            var paddingApplied = 0;
            var totalStopwatch = Stopwatch.StartNew();
            foreach (var (fileName, frames) in images)
            {
                if (TryApplyTileAlignment(frames, tileSize, backgroundColor, fileName))
                {
                    paddingApplied++;
                }
            }

            totalStopwatch.Stop();
            Log.Information("Applied padding to {PaddingCount} of {InputCount} image(s). Took: {Elapsed}ms",
                paddingApplied, images.Count, totalStopwatch.ElapsedMilliseconds);
            return true;
        }

        /// <summary>
        /// Attempts to align a collection of frames to a grid of tiles.
        /// </summary>
        /// <param name="frames">A collection of frames to align.</param>
        /// <param name="tileSize">The size of the tiles to align the frames to.</param>
        /// <param name="backgroundColor">The color of the background to use for padding.</param>
        /// <param name="fileName">The name of the file that the frames belong to.</param>
        /// <returns><see langword="true"/> if the frames were aligned, <see langword="false"/> otherwise.</returns>
        public static bool TryApplyTileAlignment(List<Image<Rgba32>> frames, Size tileSize, Rgba32 backgroundColor, string fileName)
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
                var alignedImage = new Image<Rgba32>(alignedWidth, alignedHeight);
                alignedImage.Mutate(context => context.BackgroundColor(backgroundColor));
                alignedImage.Mutate(context => context.DrawImage(initialFrame, Point.Empty, 1f));
                frames[i] = alignedImage;
            }

            if (alignedWidth == initialFrame.Width && alignedHeight == initialFrame.Height)
            {
                Log.Verbose("No padding needed for {File} ({Width}x{Height})",
                    fileName, initialFrame.Width, initialFrame.Height);
                return false;
            }

            Log.Verbose("Padding {File} ({Width}x{Height}) to ({AlignedWidth}x{AlignedHeight})",
                fileName, initialFrame.Width, initialFrame.Height, alignedWidth, alignedHeight);
            return true;
        }
    }
}
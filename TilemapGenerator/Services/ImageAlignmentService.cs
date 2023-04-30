﻿using System.Diagnostics;
using Serilog;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public sealed class ImageAlignmentService : IImageAlignmentService
{
    private readonly ILogger _logger;
    private readonly Size _tileSize;
    private readonly Rgba32 _transparentColor;

    public ImageAlignmentService(ILogger logger, ApplicationOptions options)
    {
        _logger = logger;
        _tileSize = options.TileSize;
        _transparentColor = options.TransparentColor;
    }

    /// <summary>
    /// Attempts to align a list of images to the tile size and returns whether the operation was successful.
    /// </summary>
    /// <param name="fileName">The name of the file being processed.</param>
    /// <param name="frames">The list of images to be aligned.</param>
    /// <returns>True if the operation was successful, false otherwise.</returns>
    public bool TryAlignImage(string fileName, List<Image<Rgba32>> frames)
    {
        var alignmentStopwatch = new Stopwatch();

        for (var i = 0; i < frames.Count; i++)
        {
            var frame = frames[i];
            var alignedWidth = (int)Math.Ceiling((double)frame.Width / _tileSize.Width) * _tileSize.Width;
            var alignedHeight = (int)Math.Ceiling((double)frame.Height / _tileSize.Height) * _tileSize.Height;
            var alignedFrame = new Image<Rgba32>(alignedWidth, alignedHeight);

            try
            {
                alignedFrame.Mutate(context => context.BackgroundColor(_transparentColor));
                alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));
            }
            catch (ImageProcessingException e)
            {
                _logger.Error(e, "Could not apply transformations on {FileName}", fileName);
                return false;
            }

            frames[i] = alignedFrame;
        }

        _logger.Verbose("Aligned {FrameCount} frame(s) of {FileName}. Took: {Elapsed}ms", frames.Count, fileName, alignmentStopwatch.ElapsedMilliseconds);
        return true;
    }
}
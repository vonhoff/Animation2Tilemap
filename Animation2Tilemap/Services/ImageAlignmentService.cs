using System.Diagnostics;
using Animation2Tilemap.Services.Contracts;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Animation2Tilemap.Services;

public class ImageAlignmentService(ILogger logger, ApplicationOptions options) : IImageAlignmentService
{
    private readonly Size _tileSize = options.TileSize;

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
                alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));
            }
            catch (ImageProcessingException e)
            {
                logger.Error(e, "Could not apply transformations on {FileName}", fileName);
                return false;
            }

            frames[i] = alignedFrame;
        }

        logger.Verbose("Aligned {FrameCount} frame(s) of {FileName}. Took: {Elapsed}ms", frames.Count, fileName, alignmentStopwatch.ElapsedMilliseconds);
        return true;
    }
}
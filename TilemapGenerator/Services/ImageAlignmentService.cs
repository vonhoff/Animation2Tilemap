using System.Diagnostics;
using Serilog;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class ImageAlignmentService : IImageAlignmentService
    {
        private readonly ILogger _logger;

        public ImageAlignmentService(ILogger logger)
        {
            _logger = logger;
        }

        public void AlignCollection(Dictionary<string, List<Image<Rgba32>>> images, Size tileSize, Rgba32 transparentColor)
        {
            var totalStopwatch = Stopwatch.StartNew();
            var alignmentStopwatch = new Stopwatch();
            var processedCount = 0;

            foreach (var (fileName, frames) in images)
            {
                alignmentStopwatch.Restart();
                for (var i = 0; i < frames.Count; i++)
                {
                    var frame = frames[i];
                    var alignedWidth = (int)Math.Ceiling((double)frame.Width / tileSize.Width) * tileSize.Width;
                    var alignedHeight = (int)Math.Ceiling((double)frame.Height / tileSize.Height) * tileSize.Height;
                    var alignedFrame = new Image<Rgba32>(alignedWidth, alignedHeight);

                    try
                    {
                        alignedFrame.Mutate(context => context.BackgroundColor(transparentColor));
                        alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));
                    }
                    catch (ImageProcessingException e)
                    {
                        _logger.Error(e, "Could not apply transformations on {fileName}", fileName);
                        break;
                    }

                    frames[i] = alignedFrame;
                    processedCount++;
                }

                _logger.Verbose("Aligned {frameCount} frame(s) for {fileName}. Took: {elapsed}ms",
                    frames.Count, fileName, alignmentStopwatch.ElapsedMilliseconds);
            }

            _logger.Information("Aligned {processedCount} frame(s) of {inputCount} image(s). Took: {elapsed}ms",
                processedCount, images.Count, totalStopwatch.ElapsedMilliseconds);
        }
    }
}
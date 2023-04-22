using System.Diagnostics;
using Serilog;
using TilemapGenerator.Common.CommandLine;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class ImageAlignmentService : IImageAlignmentService
    {
        private readonly ILogger _logger;
        private readonly Size _tileSize;
        private readonly Rgba32 _transparentColor;

        public ImageAlignmentService(ILogger logger, CommandLineOptions options)
        {
            _logger = logger;
            _tileSize = options.TileSize;
            _transparentColor = options.TransparentColor;
        }

        public void AlignCollection(Dictionary<string, List<Image<Rgba32>>> images)
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
                        _logger.Error(e, "Could not apply transformations on {fileName}", fileName);
                        break;
                    }

                    frames[i] = alignedFrame;
                    processedCount++;
                }

                _logger.Verbose("Aligned {frameCount} frame(s) of {fileName}. Took: {elapsed}ms",
                    frames.Count, fileName, alignmentStopwatch.ElapsedMilliseconds);
            }

            _logger.Information("Aligned a total of {processedCount} frame(s) of {inputCount} image(s). Took: {elapsed}ms",
                processedCount, images.Count, totalStopwatch.ElapsedMilliseconds);
        }
    }
}
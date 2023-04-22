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
            var stopwatch = new Stopwatch();
            foreach (var (fileName, frames) in images)
            {
                stopwatch.Restart();

                for (var i = 0; i < frames.Count; i++)
                {
                    var frame = frames[i];
                    var alignedWidth = (int)Math.Ceiling((double)frame.Width / tileSize.Width) * tileSize.Width;
                    var alignedHeight = (int)Math.Ceiling((double)frame.Height / tileSize.Height) * tileSize.Height;

                    var alignedFrame = new Image<Rgba32>(alignedWidth, alignedHeight);
                    alignedFrame.Mutate(context => context.BackgroundColor(transparentColor));
                    alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));

                    frames[i] = alignedFrame;
                }

                _logger.Information("Aligned {frameCount} frame(s) for {fileName}. Took: {elapsed}ms",
                    frames.Count, fileName, stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
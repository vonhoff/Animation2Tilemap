using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services
{
    public class ImageAlignmentService : IImageAlignmentService
    {
        public Image<Rgba32> AlignFrame(Image<Rgba32> frame, Size tileSize, Rgba32 backgroundColor)
        {
            var alignedWidth = (int)Math.Ceiling((double)frame.Width / tileSize.Width) * tileSize.Width;
            var alignedHeight = (int)Math.Ceiling((double)frame.Height / tileSize.Height) * tileSize.Height;

            var alignedFrame = new Image<Rgba32>(alignedWidth, alignedHeight);
            alignedFrame.Mutate(context => context.BackgroundColor(backgroundColor));
            alignedFrame.Mutate(context => context.DrawImage(frame, Point.Empty, 1f));

            return alignedFrame;
        }
    }
}
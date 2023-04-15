namespace TilemapGenerator.Utilities
{
    public static class ImageAlignmentUtility
    {
        /// <summary>
        /// Aligns an image frame to a tile size.
        /// </summary>
        /// <param name="frame">The image frame to align.</param>
        /// <param name="tileSize">The size of the tiles to align the frame to.</param>
        /// <param name="backgroundColor">The color of the background to apply.</param>
        /// <returns>A new image frame aligned to the tile size.</returns>
        public static Image<Rgba32> AlignFrame(Image<Rgba32> frame, Size tileSize, Rgba32 backgroundColor)
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
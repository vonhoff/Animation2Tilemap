namespace TilemapGenerator.Common
{
    public class CommandLineOptions
    {
        public CommandLineOptions(bool animation, int animationFrameDuration, string input, string output, int tileHeight, int tileWidth, string transparentColor, bool verbose)
        {
            Animation = animation;
            AnimationFrameDuration = animationFrameDuration;
            Input = input;
            Output = output;
            TileHeight = tileHeight;
            TileWidth = tileWidth;
            TransparentColor = transparentColor;
            Verbose = verbose;
        }

        public bool Animation { get; }
        public int AnimationFrameDuration { get; }
        public string Input { get; }
        public string Output { get; }
        public int TileHeight { get; }
        public int TileWidth { get; }
        public string TransparentColor { get; } 
        public bool Verbose { get; }
    }
}
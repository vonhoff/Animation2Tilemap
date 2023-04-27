﻿namespace TilemapGenerator.Common.Configuration
{
    public class ApplicationOptions
    {
        public ApplicationOptions(
            int animationFrameDuration,
            string input,
            string output,
            int tileHeight,
            int tileWidth,
            string transparentColor,
            bool verbose)
        {
            AnimationFrameDuration = animationFrameDuration;
            Input = input;
            Output = output;
            TileSize = new Size(tileWidth, tileHeight);
            TransparentColor = Rgba32.ParseHex(transparentColor);
            Verbose = verbose;
        }

        public int AnimationFrameDuration { get; }
        public string Input { get; }
        public string Output { get; }
        public Size TileSize { get; }
        public Rgba32 TransparentColor { get; }
        public bool Verbose { get; }
    }
}
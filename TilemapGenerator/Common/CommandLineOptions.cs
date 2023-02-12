using System.Text.RegularExpressions;
using CommandLine;

namespace TilemapGenerator.Common
{
    public partial class CommandLineOptions
    {
        [Option('a', "animation", HelpText = "Enable animation processing")]
        public bool Animation { get; set; }

        [Option('d', "delta", HelpText = "Animation frame duration", Default = 200)]
        public int AnimationFrameDuration { get; set; }

        [Option('i', "input", Required = true, HelpText = "Input file or folder path")]
        public string Input { get; set; } = string.Empty;

        [Option('o', "output", HelpText = "Output folder")]
        public string Output { get; set; } = string.Empty;

        [Option('h', "height", HelpText = "Tile height", Default = 8)]
        public int TileHeight { get; set; }

        [Option('w', "width", HelpText = "Tile width", Default = 8)]
        public int TileWidth { get; set; }

        [Option('t', "trans", HelpText = "Transparent color (RGBA)", Default = "00000000")]
        public string TransparentColor { get; set; } = null!;

        [Option('v', "verbose", HelpText = "Enable verbose logging")]
        public bool Verbose { get; set; }

        public void Normalize()
        {
            Input = Path.GetFullPath(Input);
            var rootDirectory = Path.GetDirectoryName(Input) ?? Environment.CurrentDirectory;
            if (string.IsNullOrWhiteSpace(Output))
            {
                var directoryName = Path.GetFileNameWithoutExtension(Input);
                Output = Path.Combine(rootDirectory, directoryName);
            }
            else if (!Path.IsPathRooted(Output))
            {
                Output = Path.Combine(rootDirectory, Output);
            }
        }

        public bool TryValidate(out string errorMessage)
        {
            if (TileHeight < 1)
            {
                errorMessage = "Tile height must be greater than or equal to 1.";
                return false;
            }

            if (TileWidth < 1)
            {
                errorMessage = "Tile width must be greater than or equal to 1.";
                return false;
            }

            if (AnimationFrameDuration < 1)
            {
                errorMessage = "The animation frame duration must be greater than or equal to 1.";
                return false;
            }

            if (!TransparentColorRegex().IsMatch(TransparentColor))
            {
                errorMessage = "The transparent color has an incorrect format.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        [GeneratedRegex("^#?([a-fA-F0-9]{3,4}){1,2}$")]
        private static partial Regex TransparentColorRegex();
    }
}
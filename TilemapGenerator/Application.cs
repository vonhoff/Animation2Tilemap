using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly IAlphanumericPatternService _alphanumericPatternService;
        private readonly IImageAlignmentService _imageAlignmentService;
        private readonly IImageLoaderService _imageLoaderService;
        private readonly ILogger _logger;
        private readonly ITilesetTileFactory _tileRecordService;
        private readonly ITilesetFactory _tilesetFactory;
        private readonly CommandLineOptions _options;

        public Application(
            IAlphanumericPatternService alphanumericPatternService,
            IImageAlignmentService imageAlignmentService,
            IImageLoaderService imageLoaderService,
            ILogger logger, 
            ITilesetTileFactory tileRecordService,
            ITilesetFactory tilesetFactory,
            CommandLineOptions options)
        {
            _alphanumericPatternService = alphanumericPatternService;
            _imageAlignmentService = imageAlignmentService;
            _imageLoaderService = imageLoaderService;
            _logger = logger;
            _tileRecordService = tileRecordService;
            _tilesetFactory = tilesetFactory;
            _options = options;
        }

        /// <summary>
        /// Loads and aligns images based on the specified command-line options.
        /// </summary>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        /// <param name="suitableForAnimation"><see langword="true"/> if all images have the same size and contain a single frame, otherwise <see langword="false"/>.</param>
        /// <returns>A value indicating whether the loading and alignment succeeded.</returns>
        public bool LoadAndAlignImages(out Dictionary<string, List<Image<Rgba32>>> images, out bool suitableForAnimation)
        {
            if (!_imageLoaderService.TryLoadImages(_options.Input, out images, out suitableForAnimation))
            {
                return false;
            }

            foreach (var (fileName, frames) in images)
            {
                for (var i = 0; i < frames.Count; i++)
                {
                    frames[i] = _imageAlignmentService.AlignFrame(frames[i], _options.TileSize, _options.TransparentColor);
                }

                _logger.Information("Aligned {frameCount} frame(s) for {fileName}", frames.Count, fileName);
            }

            return true;
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        public void Run()
        {
            if (!LoadAndAlignImages(out var images, out var suitableForAnimation))
            {
                return;
            }

            if (suitableForAnimation)
            {
                _logger.Information("The loaded image files can be used as animation frames.");

                if (_options.Animated)
                {
                    TransformImagesToAnimation(ref images);
                }
                else
                {
                    _logger.Warning("Animation processing is not requested. Images will be processed individually.");
                }
            }
            else
            {
                _logger.Warning("The loaded image files cannot be used as animation frames. " +
                                "Images will be processed individually.");
            }

            foreach (var (fileName, frames) in images)
            {
                var tileRecords = _tileRecordService.FromFrames(frames, _options.TileSize);
                var tileset = _tilesetFactory.FromTiles(tileRecords, _options.TileSize);

                var serializer = new XmlSerializer(typeof(Tileset));
                using (var memoryStream = new MemoryStream())
                {
                    var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    var settings = new XmlWriterSettings
                    {
                        Encoding = new UTF8Encoding(false), // specify UTF-8 encoding without BOM
                        Indent = true // format the output with indentation
                    };
                    using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
                    {
                        serializer.Serialize(xmlWriter, tileset, namespaces);
                    }
                    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                    Log.Information("(name: {fileName}, amount: {amount}) \n{content}", fileName, frames.Count, xml);
                }
            }
        }
        /// <summary>
        /// Groups the loaded and aligned images into a single animation.
        /// </summary>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        public void TransformImagesToAnimation(ref Dictionary<string, List<Image<Rgba32>>> images)
        {
            _logger.Information("Animation processing is enabled. Images are treated as animation frames.");

            List<string> fileNames = images.Keys.Select(Path.GetFileNameWithoutExtension).ToList()!;

            var name = _alphanumericPatternService.GetMostOccurringPattern(fileNames);
            name ??= _alphanumericPatternService.GetMostOccurringLetter(fileNames);

            if (name == null)
            {
                var fileName = "Animation_" + DateTime.Now.ToLocalTime();
                name = string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
            }

            var frames = images.Values.Select(v => v.First()).ToList();
            images = new Dictionary<string, List<Image<Rgba32>>>
            {
                { name, frames }
            };
        }
    }
}
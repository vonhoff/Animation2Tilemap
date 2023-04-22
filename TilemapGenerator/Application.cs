using Serilog;
using TilemapGenerator.CommandLine;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly IAlphanumericPatternService _alphanumericPatternService;
        private readonly IImageAlignmentService _imageAlignmentService;
        private readonly IImageLoaderService _imageLoaderService;
        private readonly ILogger _logger;
        private readonly CommandLineOptions _options;

        public Application(
            IAlphanumericPatternService alphanumericPatternService,
            IImageAlignmentService imageAlignmentService,
            IImageLoaderService imageLoaderService,
            ILogger logger,
            CommandLineOptions options)
        {
            _alphanumericPatternService = alphanumericPatternService;
            _imageAlignmentService = imageAlignmentService;
            _imageLoaderService = imageLoaderService;
            _logger = logger;
            _options = options;
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        public void Run()
        {
            if (!LoadAndAlignImages(out var images))
            {
                return;
            }

            foreach (var (fileName, frames) in images)
            {
                //var serializer = new XmlSerializer(typeof(Tileset));
                //using (var memoryStream = new MemoryStream())
                //{
                //    var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                //    var settings = new XmlWriterSettings
                //    {
                //        Encoding = new UTF8Encoding(false), // specify UTF-8 encoding without BOM
                //        Indent = true // format the output with indentation
                //    };
                //    using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
                //    {
                //        serializer.Serialize(xmlWriter, tileset, namespaces);
                //    }
                //    var xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                //    Log.Information("(name: {fileName}, amount: {amount}) \n{content}", fileName, frames.Count, xml);
                //}
            }
        }

        /// <summary>
        /// Loads and aligns images based on the specified command-line options.
        /// </summary>
        /// <param name="images">The loaded and aligned images, grouped by filename.</param>
        /// <returns>A value indicating whether the loading and alignment succeeded.</returns>
        private bool LoadAndAlignImages(out Dictionary<string, List<Image<Rgba32>>> images)
        {
            if (_imageLoaderService.TryLoadImages(_options.Input, _options.Animated, out images))
            {
                _imageAlignmentService.AlignCollection(images, _options.TileSize, _options.TransparentColor);
                return true;
            }

            return false;
        }
    }
}
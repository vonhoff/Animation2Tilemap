using System.Diagnostics;
using Serilog;
using TilemapGenerator.Common;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator;

public class Application
{
    private readonly IImageAlignmentService _imageAlignmentService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly ITilesetFactory _tilesetFactory;
    private readonly IXmlSerializerService _xmlSerializerService;
    private readonly ILogger _logger;
    private readonly string _outputFolder;

    public Application(
        IImageAlignmentService imageAlignmentService,
        IImageLoaderService imageLoaderService,
        ITilesetFactory tilesetFactory,
        IXmlSerializerService xmlSerializerService,
        ILogger logger,
        ApplicationOptions options)
    {
        _imageAlignmentService = imageAlignmentService;
        _imageLoaderService = imageLoaderService;
        _tilesetFactory = tilesetFactory;
        _xmlSerializerService = xmlSerializerService;
        _logger = logger;
        _outputFolder = options.Output;
    }

    public void Run()
    {
        if (!_imageLoaderService.TryLoadImages(out var images))
        {
            return;
        }

        foreach (var (fileName, frames) in images)
        {
            if (!_imageAlignmentService.TryAlignImage(fileName, frames))
            {
                continue;
            }

            var tilesetImageOutput = Path.Combine(_outputFolder, fileName + ".png");
            var tilesetOutput = Path.Combine(_outputFolder, fileName + ".tsx");

            try
            {
                var stopwatch = Stopwatch.StartNew();
                var tileset = _tilesetFactory.CreateFromImage(fileName, frames);
                tileset.Image.Data.SaveAsPng(tilesetImageOutput);

                var serializedTileset = _xmlSerializerService.Serialize(tileset);
                File.WriteAllText(tilesetOutput, serializedTileset);
                stopwatch.Stop();

                _logger.Information("Created Tileset files successfully for {FileName}. Took: {Elapsed}ms",
                    fileName, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save tileset files for {FileName}.", fileName);
            }
        }
    }
}
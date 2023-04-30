using System.Diagnostics;
using Serilog;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator;

public sealed class Application
{
    private readonly IImageAlignmentService _imageAlignmentService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly ITilesetFactory _tilesetFactory;
    private readonly IXmlSerializerService _xmlSerializerService;
    private readonly ILogger _logger;
    private readonly string _outputFolder;
    private readonly ITilemapFactory _tilemapFactory;

    public Application(
        IImageAlignmentService imageAlignmentService,
        IImageLoaderService imageLoaderService,
        ITilesetFactory tilesetFactory,
        ITilemapFactory tilemapFactory,
        IXmlSerializerService xmlSerializerService,
        ILogger logger,
        ApplicationOptions options)
    {
        _imageAlignmentService = imageAlignmentService;
        _imageLoaderService = imageLoaderService;
        _tilesetFactory = tilesetFactory;
        _tilemapFactory = tilemapFactory;
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
            var tilemapOutput = Path.Combine(_outputFolder, fileName + ".tmx");

            try
            {
                var tileset = _tilesetFactory.CreateFromImage(fileName, frames);
                tileset.Image.Data.SaveAsPng(tilesetImageOutput);

                var serializedTileset = _xmlSerializerService.Serialize(tileset);
                File.WriteAllText(tilesetOutput, serializedTileset);

                var tilemap = _tilemapFactory.CreateFromTileset(tileset);
                var serializedTilemap = _xmlSerializerService.Serialize(tilemap);
                File.WriteAllText(tilemapOutput, serializedTilemap);

                _logger.Information("Created tileset and tilemap for {fileName}", fileName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save files for {FileName}.", fileName);
            }
        }
    }
}
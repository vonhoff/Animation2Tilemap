using System.Diagnostics;
using Animation2Tilemap.Factories.Contracts;
using Animation2Tilemap.Services.Contracts;
using Serilog;

namespace Animation2Tilemap;

public class Application
{
    private readonly IImageAlignmentService _imageAlignmentService;
    private readonly IImageLoaderService _imageLoaderService;
    private readonly ITilesetFactory _tilesetFactory;
    private readonly ITilemapFactory _tilemapFactory;
    private readonly IXmlSerializerService _xmlSerializerService;
    private readonly ILogger _logger;
    private readonly string _outputFolder;

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

        var successfulImages = 0;
        Parallel.ForEach(images, frameCollection =>
        {
            var fileName = frameCollection.Key;
            var frames = frameCollection.Value;

            _logger.Verbose("Processing image {FileName} with {FrameCount} frame(s).", fileName, frames.Count);
            var totalStopwatch = Stopwatch.StartNew();

            try
            {
                if (!_imageAlignmentService.TryAlignImage(fileName, frames))
                {
                    return;
                }

                var taskStopwatch = Stopwatch.StartNew();
                var tileset = _tilesetFactory.CreateFromImage(fileName, frames);
                _logger.Verbose("Created tileset from {FileName}. Took: {Elapsed}ms",
                    fileName, taskStopwatch.ElapsedMilliseconds);

                taskStopwatch.Restart();
                var tilemap = _tilemapFactory.CreateFromTileset(tileset);
                _logger.Verbose("Created tilemap from tileset {FileName}. Took: {Elapsed}ms",
                    fileName, taskStopwatch.ElapsedMilliseconds);

                var tilesetImageOutput = Path.Combine(_outputFolder, fileName + ".png");
                var tilesetOutput = Path.Combine(_outputFolder, fileName + ".tsx");
                var tilemapOutput = Path.Combine(_outputFolder, fileName + ".tmx");

                _logger.Verbose("Saving files for {FileName} to {OutputFolder}", fileName, _outputFolder);
                tileset.Image.Data.SaveAsPng(tilesetImageOutput);
                File.WriteAllText(tilesetOutput, _xmlSerializerService.Serialize(tileset));
                File.WriteAllText(tilemapOutput, _xmlSerializerService.Serialize(tilemap));

                totalStopwatch.Stop();
                _logger.Information("Successfully processed {FileName} to {OutputFolder}. Took: {Elapsed}ms",
                    fileName, _outputFolder, totalStopwatch.ElapsedMilliseconds);
                Interlocked.Increment(ref successfulImages);
            }
            catch (Exception ex)
            {
                totalStopwatch.Stop();
                _logger.Error(ex, "Failed to process image {FileName}. Took: {Elapsed}ms", fileName, totalStopwatch.ElapsedMilliseconds);
            }
        });

        _logger.Information("Finished. {SuccessfulImages} of {TotalImages} images were successfully processed.", successfulImages, images.Count);
    }
}
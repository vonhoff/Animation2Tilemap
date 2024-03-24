using Animation2Tilemap.Core.Enums;
using Animation2Tilemap.Core.Factories.Contracts;
using Animation2Tilemap.Core.Services.Contracts;
using Serilog;
using SixLabors.ImageSharp;
using System.Diagnostics;

namespace Animation2Tilemap.Core;

public class Application(
    IImageAlignmentService imageAlignmentService,
    IImageLoaderService imageLoaderService,
    ITilesetFactory tilesetFactory,
    ITilemapFactory tilemapFactory,
    IXmlSerializerService xmlSerializerService,
    ILogger logger,
    ApplicationOptions options)
{
    private readonly string _outputFolder = Path.GetFullPath(options.Output);

    public ApplicationResult Run()
    {
        if (imageLoaderService.TryLoadImages(out var images) == false)
        {
            return ApplicationResult.Failed;
        }

        Directory.CreateDirectory(_outputFolder);

        var successfulImages = 0;
        Parallel.ForEach(images, frameCollection =>
        {
            var fileName = frameCollection.Key;
            var frames = frameCollection.Value;

            logger.Verbose("Processing image {FileName} with {FrameCount} frame(s).", fileName, frames.Count);
            var totalStopwatch = Stopwatch.StartNew();

            try
            {
                if (imageAlignmentService.TryAlignImage(fileName, frames) == false)
                {
                    return;
                }

                var taskStopwatch = Stopwatch.StartNew();
                var tileset = tilesetFactory.CreateFromImage(fileName, frames);
                logger.Verbose("Created tileset from {FileName}. Took: {Elapsed}ms",
                    fileName, taskStopwatch.ElapsedMilliseconds);

                taskStopwatch.Restart();
                var tilemap = tilemapFactory.CreateFromTileset(tileset);
                logger.Verbose("Created tilemap from tileset {FileName}. Took: {Elapsed}ms",
                    fileName, taskStopwatch.ElapsedMilliseconds);

                var tilesetImageOutput = Path.Combine(_outputFolder, fileName + ".png");
                var tilesetOutput = Path.Combine(_outputFolder, fileName + ".tsx");
                var tilemapOutput = Path.Combine(_outputFolder, fileName + ".tmx");

                logger.Verbose("Saving files for {FileName} to {OutputFolder}", fileName, _outputFolder);
                tileset.Image.Data.SaveAsPng(tilesetImageOutput);
                File.WriteAllText(tilesetOutput, xmlSerializerService.Serialize(tileset));
                File.WriteAllText(tilemapOutput, xmlSerializerService.Serialize(tilemap));

                totalStopwatch.Stop();
                logger.Information("Successfully processed {FileName} to {OutputFolder}. Took: {Elapsed}ms",
                    fileName, _outputFolder, totalStopwatch.ElapsedMilliseconds);
                Interlocked.Increment(ref successfulImages);
            }
            catch (Exception ex)
            {
                totalStopwatch.Stop();
                logger.Error(ex, "Failed to process image {FileName}. Took: {Elapsed}ms", fileName, totalStopwatch.ElapsedMilliseconds);
            }
        });

        logger.Information("Finished. {SuccessfulImages} of {TotalImages} images were successfully processed.", successfulImages, images.Count);

        if (successfulImages == 0)
        {
            return ApplicationResult.Failed;
        }

        return successfulImages == images.Count ? ApplicationResult.Successful : ApplicationResult.PartiallySuccessful;
    }
}
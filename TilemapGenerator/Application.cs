using Serilog;
using TilemapGenerator.Entities;
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly IImageAlignmentService _imageAlignmentService;
        private readonly IImageLoaderService _imageLoaderService;
        private readonly ITilesetFactory _tilesetFactory;
        private readonly ITilesetSerializerService _tilesetSerializerService;

        public Application(
            IImageAlignmentService imageAlignmentService,
            IImageLoaderService imageLoaderService,
            ITilesetFactory tilesetFactory,
            ITilesetSerializerService tilesetSerializerService)
        {
            _imageAlignmentService = imageAlignmentService;
            _imageLoaderService = imageLoaderService;
            _tilesetFactory = tilesetFactory;
            _tilesetSerializerService = tilesetSerializerService;
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

                var tileset = _tilesetFactory.CreateFromImage(fileName, frames);
                var output = _tilesetSerializerService.Serialize(tileset);
                Log.Information("Content: \n{Content}", output);
            }
        }
    }
}
using TilemapGenerator.Factories.Contracts;
using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator
{
    public class Application
    {
        private readonly IImageAlignmentService _imageAlignmentService;
        private readonly IImageLoaderService _imageLoaderService;

        public Application(
            IImageAlignmentService imageAlignmentService,
            IImageLoaderService imageLoaderService)
        {
            _imageAlignmentService = imageAlignmentService;
            _imageLoaderService = imageLoaderService;
        }

        public void Run()
        {
            if (!_imageLoaderService.TryLoadImages(out var images))
            {
                return;
            }

            _imageAlignmentService.AlignImageCollection(images);
        }
    }
}
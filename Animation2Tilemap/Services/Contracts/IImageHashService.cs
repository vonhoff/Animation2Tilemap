namespace Animation2Tilemap.Services.Contracts;

public interface IImageHashService
{
    int Compute(Image<Rgba32> image);
}
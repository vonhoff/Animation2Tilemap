using Animation2Tilemap.Core.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Animation2Tilemap.Core.Test.Services;

public class ImageHashServiceTests
{
    private readonly ImageHashService _imageHashService = new();

    [Fact]
    public void Compute_ShouldReturnSameHash_GivenIdenticalImages()
    {
        // Arrange
        var image1 = new Image<Rgba32>(16, 16);
        var image2 = new Image<Rgba32>(16, 16);

        // Act
        var hash1 = _imageHashService.Compute(image1);
        var hash2 = _imageHashService.Compute(image2);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void Compute_ShouldReturnDifferentHash_GivenDifferentImages()
    {
        // Arrange
        var image1 = new Image<Rgba32>(16, 16);
        var image2 = new Image<Rgba32>(16, 16);
        image2[0, 0] = Rgba32.ParseHex("FF0000");

        // Act
        var hash1 = _imageHashService.Compute(image1);
        var hash2 = _imageHashService.Compute(image2);

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}
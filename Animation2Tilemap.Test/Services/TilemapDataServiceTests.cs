using Animation2Tilemap.Enums;
using Animation2Tilemap.Services;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Test.Services;

public class TilemapDataServiceTests
{
    private readonly ITilemapDataService _tilemapDataService;
    private readonly string _base64WithZlib;
    private readonly string _base64WithGzip;
    private readonly string _base64;
    private readonly string _csvData;
    private readonly uint[] _tilemapData;

    public TilemapDataServiceTests()
    {
        _tilemapDataService = new TilemapDataService();
        _base64 = TestResourcesHelper.ImportText("Base64.txt");
        _base64WithZlib = TestResourcesHelper.ImportText("Base64Zlib.txt");
        _base64WithGzip = TestResourcesHelper.ImportText("Base64Gzip.txt");
        _csvData = TestResourcesHelper.ImportText("TilemapData.csv");
        _tilemapData = TestResourcesHelper.ImportArrayFromJson<uint>("Tilemap.json");
    }

    [Fact]
    public void ParseData_ShouldHaveCorrectResult_WithoutCompression()
    {
        // Act
        var tilemapData = _tilemapDataService.ParseData(_base64, TileLayerFormat.Base64Uncompressed);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void ParseData_ShouldHaveCorrectResult_WhenUsingZLib()
    {
        // Act
        var tilemapData = _tilemapDataService.ParseData(_base64WithZlib, TileLayerFormat.Base64ZLib);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void ParseData_ShouldHaveCorrectResult_WhenUsingGzip()
    {
        // Act
        var tilemapData = _tilemapDataService.ParseData(_base64WithGzip, TileLayerFormat.Base64GZip);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void ParseData_ShouldHaveCorrectResult_WhenUsingCSV()
    {
        // Act
        var tilemapData = _tilemapDataService.ParseData(_csvData, TileLayerFormat.CSV);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void SerializeData_ShouldHaveCorrectResult_WithoutCompression()
    {
        // Act
        var serialized = _tilemapDataService.SerializeData(_tilemapData, TileLayerFormat.Base64Uncompressed);
        var tilemapData = _tilemapDataService.ParseData(serialized, TileLayerFormat.Base64Uncompressed);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void SerializeData_ShouldHaveCorrectResult_WhenUsingZLib()
    {
        // Act
        var serialized = _tilemapDataService.SerializeData(_tilemapData, TileLayerFormat.Base64ZLib);
        var tilemapData = _tilemapDataService.ParseData(serialized, TileLayerFormat.Base64ZLib);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void SerializeData_ShouldHaveCorrectResult_WhenUsingGzip()
    {
        // Act
        var serialized = _tilemapDataService.SerializeData(_tilemapData, TileLayerFormat.Base64ZLib);
        var tilemapData = _tilemapDataService.ParseData(serialized, TileLayerFormat.Base64ZLib);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }

    [Fact]
    public void SerializeData_ShouldHaveCorrectResult_WhenUsingCSV()
    {
        // Act
        var serialized = _tilemapDataService.SerializeData(_tilemapData, TileLayerFormat.CSV);
        var tilemapData = _tilemapDataService.ParseData(serialized, TileLayerFormat.CSV);

        // Assert
        Assert.Equal(_tilemapData, tilemapData);
    }
}
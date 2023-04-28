using System.Diagnostics;
using System.Text.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace TilemapGenerator.Test;

internal static class TestResourcesHelper
{
    /// <summary>
    /// Export an array of any struct type to a JSON file.
    /// </summary>
    public static void ExportArray<T>(T[] array, string fileName) where T : struct
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", callerClassName, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        using var fs = new FileStream(filePath, FileMode.Create);
        JsonSerializer.SerializeAsync(fs, array);
    }

    /// <summary>
    /// Import an array of any struct type from a JSON file.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static T[] ImportArray<T>(string jsonFile) where T : struct
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine("Resources", callerClassName, jsonFile);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }
        using var fs = new FileStream(filePath, FileMode.Open);
        var array = JsonSerializer.DeserializeAsync<T[]>(fs).Result;
        return array!;
    }

    /// <summary>
    /// Export an ImageSharp image to a PNG file.
    /// </summary>
    public static void ExportImage(Image<Rgba32> image, string fileName)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", callerClassName, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        using var fs = new FileStream(filePath, FileMode.Create);
        image.Save(fs, new PngEncoder());
    }

    /// <summary>
    /// Import an image file.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static Image<Rgba32> ImportImage(string fileName)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine("Resources", callerClassName, fileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }
        using var fs = new FileStream(filePath, FileMode.Open);
        return Image.Load<Rgba32>(fs);
    }

    /// <summary>
    /// Export text to a file.
    /// </summary>
    public static void ExportText(string text, string fileName)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", callerClassName, fileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        using var sw = new StreamWriter(filePath);
        sw.Write(text);
    }

    /// <summary>
    /// Import text from a file.
    /// </summary>
    /// <exception cref="FileNotFoundException"></exception>
    public static string ImportText(string fileName)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine("Resources", callerClassName, fileName);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }
        using var sr = new StreamReader(filePath);
        return sr.ReadToEnd();
    }
}
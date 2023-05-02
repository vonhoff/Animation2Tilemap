using System.Diagnostics;
using System.Text.Json;

namespace Animation2Tilemap.Test;

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

    /// <summary>
    /// Gets the path to a file or folder in the caller's resource folder.
    /// </summary>
    /// <param name="location">The name of the file or folder to get the path for.</param>
    /// <returns>The path to the file.</returns>
    public static string GetPath(string location)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var resourceFolder = Path.Combine("Resources", callerClassName);
        var path = Path.Combine(resourceFolder, location);
        if (File.Exists(path) || Directory.Exists(path))
        {
            return path;
        }

        throw new FileNotFoundException($"The file or folder {location} does not exist in the resource folder for {callerClassName}.");
    }
}
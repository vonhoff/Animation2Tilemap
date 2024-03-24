using System.Diagnostics;
using System.Text.Json;

namespace Animation2Tilemap.Core.Test.TestHelpers;

public static class TestResourcesHelper
{
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

    public static T[] ImportArrayFromJson<T>(string jsonFile) where T : struct
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine("Resources", callerClassName, jsonFile);
        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }

        using var fs = new FileStream(filePath, FileMode.Open);
        return JsonSerializer.Deserialize<T[]>(fs) ?? Array.Empty<T>();
    }

    public static string ImportText(string fileName)
    {
        var callerClassName = new StackFrame(1).GetMethod()!.DeclaringType!.Name;
        var filePath = Path.Combine("Resources", callerClassName, fileName);
        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }

        using var sr = new StreamReader(filePath);
        return sr.ReadToEnd();
    }
}
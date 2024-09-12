namespace Animation2Tilemap.WinForms.Extensions;

public static class ColorExtensions
{
    public static string ToHex(this Color c)
    {
        return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
    }

    public static Color ContrastColor(this Color c)
    {
        var luminance = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B) / 255;
        return luminance > 0.5 ? Color.Black : Color.White;
    }
}
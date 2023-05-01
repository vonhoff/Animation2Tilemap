using Serilog.Sinks.SystemConsole.Themes;

namespace Animation2Tilemap.Common;

public static class SerilogConsoleThemes
{
    public static SystemConsoleTheme CustomLiterate { get; } = new(
        new Dictionary<ConsoleThemeStyle, SystemConsoleThemeStyle>
        {
            [ConsoleThemeStyle.Text] = new() { Foreground = ConsoleColor.White },
            [ConsoleThemeStyle.SecondaryText] = new() { Foreground = ConsoleColor.Gray },
            [ConsoleThemeStyle.TertiaryText] = new() { Foreground = ConsoleColor.DarkGray },
            [ConsoleThemeStyle.Invalid] = new() { Foreground = ConsoleColor.Magenta },
            [ConsoleThemeStyle.Null] = new() { Foreground = ConsoleColor.Blue },
            [ConsoleThemeStyle.Name] = new() { Foreground = ConsoleColor.Gray },
            [ConsoleThemeStyle.String] = new() { Foreground = ConsoleColor.Cyan },
            [ConsoleThemeStyle.Number] = new() { Foreground = ConsoleColor.Yellow },
            [ConsoleThemeStyle.Boolean] = new() { Foreground = ConsoleColor.Blue },
            [ConsoleThemeStyle.Scalar] = new() { Foreground = ConsoleColor.Green },
            [ConsoleThemeStyle.LevelVerbose] = new() { Foreground = ConsoleColor.Gray },
            [ConsoleThemeStyle.LevelDebug] = new() { Foreground = ConsoleColor.Gray },
            [ConsoleThemeStyle.LevelInformation] = new() { Foreground = ConsoleColor.DarkCyan },
            [ConsoleThemeStyle.LevelWarning] = new() { Foreground = ConsoleColor.Yellow },
            [ConsoleThemeStyle.LevelError] = new() { Foreground = ConsoleColor.White, Background = ConsoleColor.Red },
            [ConsoleThemeStyle.LevelFatal] = new() { Foreground = ConsoleColor.White, Background = ConsoleColor.Red }
        });
}
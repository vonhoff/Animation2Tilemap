using System.CommandLine;

namespace Animation2Tilemap.CommandLineOptions.Contracts;

public interface ICommandLineOption<T>
{
    Option<T> Option { get; }

    Option<T> Register(Command command);
}
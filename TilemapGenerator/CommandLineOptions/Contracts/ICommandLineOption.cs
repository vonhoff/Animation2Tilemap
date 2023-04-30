using System.CommandLine;

namespace TilemapGenerator.CommandLineOptions.Contracts;

public interface ICommandLineOption<T>
{
    Option<T> Option { get; }

    Option<T> Register(Command command);
}
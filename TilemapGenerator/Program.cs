using System.Text;
using CommandLine;
using TilemapGenerator.Common;

namespace TilemapGenerator
{
    public static class Program
    {
        public static void Main(string[] arguments)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Parser.Default.ParseArguments<CommandLineOptions>(arguments)
                .WithParsed(options =>
                {
                    options.Normalize();

                    if (!options.TryValidate(out var errorMessage))
                    {
                        Console.WriteLine("ERROR(S): " + errorMessage);
                        return;
                    }

                    var app = new Application(options);
                    app.Run();
                });
        }
    }
}
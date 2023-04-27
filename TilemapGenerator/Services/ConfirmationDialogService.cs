using TilemapGenerator.Services.Contracts;

namespace TilemapGenerator.Services;

public class ConfirmationDialogService : IConfirmationDialogService
{
    public bool Confirm(string message, bool defaultOption)
    {
        var isValidInput = false;
        var response = defaultOption;
        while (!isValidInput)
        {
            Console.WriteLine();
            var defaultText = defaultOption ? "[Y]" : "[N]";
            Console.Write(message + " (Y/N) " + defaultText + ": ");
            var input = Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(input))
            {
                response = defaultOption;
                isValidInput = true;
            }
            else switch (input)
            {
                case "Y":
                    response = true;
                    isValidInput = true;
                    break;

                case "N":
                    response = false;
                    isValidInput = true;
                    break;

                default:
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                    break;
            }
        }

        return response;
    }
}
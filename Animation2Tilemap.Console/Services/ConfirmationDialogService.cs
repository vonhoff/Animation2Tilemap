using Animation2Tilemap.Core.Services.Contracts;

namespace Animation2Tilemap.Console.Services;

public class ConfirmationDialogService : IConfirmationDialogService
{
    public bool Confirm(string message, bool defaultOption)
    {
        var isValidInput = false;
        var response = defaultOption;
        while (isValidInput == false)
        {
            System.Console.WriteLine();
            var defaultText = defaultOption ? "[Y]" : "[N]";
            System.Console.Write(message + " (Y/N) " + defaultText + ": ");
            var input = System.Console.ReadLine()?.Trim().ToUpper();

            if (string.IsNullOrEmpty(input))
            {
                response = defaultOption;
                isValidInput = true;
            }
            else
            {
                switch (input)
                {
                    case "Y":
                    {
                        response = true;
                        isValidInput = true;
                        break;
                    }
                    case "N":
                    {
                        response = false;
                        isValidInput = true;
                        break;
                    }
                    default:
                    {
                        System.Console.WriteLine("Invalid input. Please enter Y or N.");
                        break;
                    }
                }
            }
        }

        return response;
    }
}
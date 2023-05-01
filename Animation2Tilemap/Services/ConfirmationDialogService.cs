using System.CommandLine;
using Animation2Tilemap.Services.Contracts;

namespace Animation2Tilemap.Services;

public sealed class ConfirmationDialogService : IConfirmationDialogService
{
    /// <summary>
    /// Prompts the user with a message and a default option of Yes or No and returns the user's response.
    /// </summary>
    /// <param name="message">The message to display to the user.</param>
    /// <param name="defaultOption">The default option to display to the user. If true, "Y" is the default. If false, "N" is the default.</param>
    /// <returns>A boolean value representing the user's response. True if the user selected Yes, false if they selected No.</returns>
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
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                    break;
                }
            }
        }

        return response;
    }
}
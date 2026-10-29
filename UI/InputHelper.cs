using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.UI;

//reads and validates user input
public static class InputHelper
{
    public static string ReadText(string prompt, string emptyInputMessage)
    {
        while (true)
        {
            Console.Write(prompt);
            string? userInput = Console.ReadLine();

            // ReadLine() returns null when the input stream is closed (Ctrl+Z or redirected input has ended).
            if (userInput == null)
            {
                throw new InputClosedException();
            }

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                return userInput.Trim();
            }

            ShowError(emptyInputMessage);

        }


    }

    public static string ReadTextOrCancel(string prompt, string emptyInputMessage)
    {
        while (true)
        {
            string userInput = ReadText(prompt, emptyInputMessage);
            if (userInput.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                throw new UserCancelledException();
            }
            else
            {
                return userInput;
            }

        }
    }

    public static decimal ReadPrice(string prompt, string emptyInputMessage, string invalidPriceMessage)
    {
        while (true)
        {
            string userInput = ReadText(prompt, emptyInputMessage);
            if (userInput.Equals("q", StringComparison.OrdinalIgnoreCase))
            {
                throw new UserCancelledException();
            }
            else if (Decimal.TryParse(userInput, out decimal price) && Product.IsValidPrice(price))
            {
                return price;

            }
            else
            {
                ShowError(invalidPriceMessage);
            }

        }
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

}

using ProductListManagementSystem.Exceptions;
using ProductListManagementSystem.Models;

namespace ProductListManagementSystem.UI;

// Reads and validates user input; prints colored messages
public static class InputHelper
{
    // Ask until the user enters non-empty text
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

    // Like ReadText, but "q" cancels the current operation
    public static string ReadTextOrCancel(string prompt, string emptyInputMessage)
    {
        string userInput = ReadText(prompt, emptyInputMessage);
        if (userInput.Equals("q", StringComparison.OrdinalIgnoreCase))
        {
            throw new UserCancelledException();
        }
        return userInput;
    }

    // Ask until the price is valid
    public static decimal ReadPrice(string prompt, string emptyInputMessage, string invalidPriceMessage)
    {
        while (true)
        {
            string userInput = ReadTextOrCancel(prompt, emptyInputMessage);
            // Not a number, <= 0 or more than 2 decimals -> ask again
            if (Decimal.TryParse(userInput, out decimal price) && Product.IsValidPrice(price))
            {
                return price;

            }
            else
            {
                ShowError(invalidPriceMessage);
            }

        }
    }

    // Colored messages: red = error, green = success, blue = info, yellow = warning
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

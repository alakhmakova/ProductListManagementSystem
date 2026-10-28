using ProductListManagementSystem.Exceptions;

namespace ProductListManagementSystem.UI;

//reads and validates user input
public static class InputHelper
{
    public static string ReadText(string prompt, string message)
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

            ShowError(message);

        }


    }

    private static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

}

using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage(); // Display the welcome message to the user

        string userName = PromptUserName(); // Prompt the user for their name
        int userNumber = PromptUserNumber(); // Prompt the user for their favorite number

        int squaredNumber = SquareNumber(userNumber); // Calculate the square of the user's number

        DisplayResult(userName, squaredNumber); // Display the result to the user
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!"); // Welcomes the user
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name: "); // Ask for the user's name
        return Console.ReadLine(); // Read and return the user's input
    }

    static int PromptUserNumber()
    {
         // Variable to store the user's number
        int number; 
        while (true) 
        {
            // Prompt for a favorite number
            Console.Write("Please enter your favorite number: "); 
            string userResponse = Console.ReadLine(); 

            // Attempt to parse the input as an integer
            if (int.TryParse(userResponse, out number))
            {
                // Return the valid number
                return number; 
            }
            else
            {
                // Inform the user about invalid input
                Console.WriteLine("That's not a valid number. Please try again."); 
            }
        }
    }

    static int SquareNumber(int number)
    {
         // Calculate and return the square of the number
        return number * number;
    }

    static void DisplayResult(string name, int square)
    {
        // Display the result to the user
        Console.WriteLine($"{name}, the square of your number is {square}."); 
    }
}

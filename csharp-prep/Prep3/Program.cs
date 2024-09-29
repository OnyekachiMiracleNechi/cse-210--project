using System;

class Program
{
    static void Main(string[] args)
    {
        // Let's kick off the game and have some fun!
        PlayGuessingGame();
    }

    static void PlayGuessingGame()
    {
        // Generating the magic number for you to guess!
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        int guess = -1;
        int numberOfGuesses = 0;

        Console.WriteLine("Welcome to the Magic Number Guessing Game!");
        Console.WriteLine("Your goal is to guess the magic number between 1 and 100.");

        
        while (guess != magicNumber)
        {
            Console.Write("Take a shot! What’s your guess? ");
            // Making sure the user enters a valid number
            if (!int.TryParse(Console.ReadLine(), out guess) || guess < 1 || guess > 100)
            {
                Console.WriteLine("Oops! Please enter a number between 1 and 100.");
                continue; 
            }

            numberOfGuesses++;

            // Giving the user a hint
            if (magicNumber > guess)
            {
                Console.WriteLine("Higher! You’re getting closer!");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("Lower! You're almost there!");
            }
            else
            {
                Console.WriteLine($"Congratulations! You guessed it! The magic number was {magicNumber}.");
                Console.WriteLine($"It took you {numberOfGuesses} guesses. Great job!");
            }
        }

        // Prompt for continuation
        Console.WriteLine("Want to play again? (yes/no)");
        string playAgain = Console.ReadLine().ToLower();

        if (playAgain == "yes")
        {
        
            PlayGuessingGame(); 
        }
        else
        {
            Console.WriteLine("Thanks for playing! Hope to see you again soon!");
        }
    }
}

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Ask for the user's name to personalize the experience
        Console.Write("What is your name? ");
        string userName = Console.ReadLine();

        List<int> numbers = new List<int>();
        
        // Generating a number to enter for calculation.
        int userNumber = -1;
        while (userNumber != 0)
        {
            Console.Write($"Enter a number (0 to calculate, {userName}): ");
            
            string userResponse = Console.ReadLine();
            
            // Checking if the input is a valid integer
            if (int.TryParse(userResponse, out userNumber))
            {
                // Only add the number if it's not 0
                if (userNumber != 0)
                {
                    numbers.Add(userNumber);
                }
            }
            else
            {
                Console.WriteLine("Oops! That's not a valid number. Please try again.");
            }
        }

        // Part 1: Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            // Add each number to the sum
            sum += number; 
        }

        Console.WriteLine($"The total sum of your numbers is: {sum}");

        // Part 2: Compute the average
        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average of your numbers is: {average}");

        // Part 3: Find the maximum number
        int max = numbers[0];
        foreach (int number in numbers)
        {
            if (number > max)
            {
                // Update the max if we find a larger number
                max = number; 
            }
        }

        Console.WriteLine($"The highest number you entered is: {max}");
        //Goodbye prompt
        Console.WriteLine($"Thanks for playing, {userName}!"); 
    }
}

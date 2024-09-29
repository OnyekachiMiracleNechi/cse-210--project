using System;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the user's name
        Console.WriteLine("What is your name?");
        string name = Console.ReadLine();

        // Prompt for the subject
        Console.WriteLine("What subject is this grade for?");
        string subject = Console.ReadLine();

        // Prompt for the user's percentage
        Console.WriteLine($"Hi {name}, what is your grade percentage for {subject}? ");
        string answer = Console.ReadLine();
        int percent;
        if (!int.TryParse(answer, out percent) || percent < 0 || percent > 100)
        {
            Console.WriteLine("Please enter a valid percentage between 0 and 100.");
            return; // Exit the program if the input is invalid
        }

        string remark = "";

        if (percent >= 90)
        {
            remark = "A";
        }
        else if (percent >= 80)
        {
            remark = "B";
        }
        else if (percent >= 70)
        {
           remark = "C";
        }
        else if (percent >= 60)
        {
            remark = "D";
        }
        else
        {
            remark = "F";
        }

        Console.WriteLine($"Your grade is: {remark}");
        
        if (percent >= 70)
        {
            Console.WriteLine("You passed! Well-done");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}
using System;

class Program
{
    static void Main(string[] args)
    {
        //Pronpt for the user's percentage
        Console.WriteLine("What is your grade percentage? ");
         string answer = Console.ReadLine();
        int percent = int.Parse(answer);

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
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}
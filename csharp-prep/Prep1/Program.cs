using System;

class Program
{
    static void Main(string[] args)
    {
        // command for user's first name.
        Console.Write("What is your first name? ");
        string first =Console.ReadLine();
       
       // Command for user's last name.
        Console.Write("What is your last name? ");
        string last = Console.ReadLine();

        Console.WriteLine($" Your name is {last}, {first} {last}.");
    }
}
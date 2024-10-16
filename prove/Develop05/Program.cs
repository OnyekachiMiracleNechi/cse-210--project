using System;
using System.Collections.Generic;
using System.Threading;

// Creativity Added:

// 1. Custom Reflection Questions:
// Added a feature that allows users to personalize their reflection experience. 
// Users can choose to add their own reflection questions during the Reflection Activity, 
// making the activity more meaningful and relevant to their personal experiences. 
// This enhances engagement by allowing users to include questions that resonate with them.

// 2. Dynamic Breathing Animation:
// Improved the breathing activity by introducing a dynamic animation that visually represents 
// the breathing process. The text expands and contracts as users breathe in and out, 
// simulating the inhale and exhale. This animation starts quickly and slows down at the end 
// of each breath, helping users synchronize their breathing with the visuals. 
// This feature creates a more calming and immersive experience, promoting relaxation effectively.



public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an activity (choose a number):");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            // Prompting the user for input
            Console.Write("Enter the number of your choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                var breathing = new BreathingActivity();
                breathing.Start();
                breathing.Execute();
                breathing.End();
            }
            else if (choice == "2")
            {
                var reflection = new ReflectionActivity();
                reflection.Start();
                reflection.Execute();
                reflection.End();
            }
            else if (choice == "3")
            {
                var listing = new ListingActivity();
                listing.Start();
                listing.Execute();
                listing.End();
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}

public abstract class Activity
{
    protected int duration;
    protected string description;

    public void Start()
    {
        Console.WriteLine($"Starting activity: {description}");
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Get ready to start...");
        Pause(3);
    }

    public void End()
    {
        Console.WriteLine($"Good job! You've completed the activity for {duration} seconds.");
        Pause(3);
    }

    protected void Pause(int seconds)
    {
        Thread.Sleep(seconds * 1000);
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        description = "This activity will help you relax by walking you through breathing in and out slowly.";
    }

    public void Execute()
    {
        for (int i = 0; i < duration; i += 4)
        {
            AnimateBreath("Breathe in...", 2);
            Pause(2);
            AnimateBreath("Breathe out...", 2);
            Pause(2);
        }
    }

    private void AnimateBreath(string message, int duration)
    {
        int steps = 10; // Number of steps in the animation
        int pauseBetweenSteps = duration * 1000 / steps; // Calculate pause duration

        for (int i = 1; i <= steps; i++)
        {
            Console.Clear();
            int size = (int)Math.Round(i * 2.0); // Expand size
            Console.ForegroundColor = ConsoleColor.Blue; // Inhale color
            Console.WriteLine(message);
            Console.WriteLine(new string('O', size)); // Animate by growing 'O'
            Thread.Sleep(pauseBetweenSteps);
        }
        for (int i = steps; i >= 1; i--)
        {
            Console.Clear();
            int size = (int)Math.Round(i * 2.0);
            Console.ForegroundColor = ConsoleColor.Green; // Exhale color
            Console.WriteLine(message);
            Console.WriteLine(new string('O', size)); // Shrink the 'O'
            Thread.Sleep(pauseBetweenSteps);
        }
        Console.ResetColor(); // Reset to default color
    }
}

public class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need."
    };

    private List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?"
    };

    public ReflectionActivity()
    {
        description = "This activity will help you reflect on times in your life when you have shown strength.";
    }

    public void Execute()
    {
        Console.WriteLine("Would you like to add your own reflection questions? (yes/no)");
        string response = Console.ReadLine().ToLower();
        
        if (response == "yes")
        {
            Console.WriteLine("Great! Please enter your reflection questions (type 'done' when finished):");
            while (true)
            {
                string question = Console.ReadLine();
                if (question.ToLower() == "done") break;
                questions.Add(question);
            }
            Console.WriteLine("Thank you! Your questions have been added.");
        }
        else
        {
            Console.WriteLine("No problem! We'll use the pre-defined questions.");
        }

        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(3);

        for (int i = 0; i < duration / 4; i++)
        {
            string question = questions[rand.Next(questions.Count)];
            Console.WriteLine(question);
            Pause(4);
        }
    }
}

public class ListingActivity : Activity
{
    private List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        description = "This activity will help you reflect on the good things in your life.";
    }

    public void Execute()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine(prompt);
        Pause(3);

        List<string> items = new List<string>();
        Console.WriteLine("Start listing items (type 'done' to finish):");

        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (item.ToLower() == "done") break;
            items.Add(item);
        }

        Console.WriteLine($"You listed {items.Count} items.");
    }
}

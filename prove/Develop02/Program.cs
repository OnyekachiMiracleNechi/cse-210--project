using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Date { get; }
    public string Prompt { get; }
    public string Response { get; }

    //Takes the date, prompt, and user response as parameters and assigns them to the corresponding properties
    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

   //Formats the output to include the date, prompt, and response, which is useful for displaying journal entries.
    public override string ToString()
    {
        return $"{Date} | {Prompt} | {Response}";
    }
}

public class Journal
{
    //Generating  a list that stores all journal entries.
    private List<Entry> entries = new List<Entry>();
    private readonly string[] prompts = new string[]
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random random = new Random();

    public string GetRandomPrompt()
    //Utilizes the Random instance to generate a random index.
    {    
        return prompts[random.Next(prompts.Length)];
    }

   //Generates the current date and adds a new Entry object to the entries list.
    public void AddEntry(string response, string prompt)
    {
        string date = DateTime.Now.ToShortDateString();
        entries.Add(new Entry(date, prompt, response));
    }

    
    //Prints each entry's string representation to the console.
    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date}~{entry.Prompt}~{entry.Response}");
            }
        }
    }

    //Clears the current entries and loads them from a specified file.
    public void LoadFromFile(string filename)
    {
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split('~');
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
        }
    }
}

public class Program
{
    private static Journal journal = new Journal();

    public static void Main(string[] args)
    {
        while (true)
        {  //generating the commands to use
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to file");
            Console.WriteLine("4. Load journal from file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                string prompt = journal.GetRandomPrompt();
                Console.WriteLine($"Prompt: {prompt}");
                Console.Write("Enter your response: ");
                string response = Console.ReadLine();
                journal.AddEntry(response, prompt);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Journal Entries:");
                journal.DisplayEntries();
            }
            else if (choice == "3")
            {
                Console.Write("Enter filename to save: ");
                string saveFilename = Console.ReadLine();
                journal.SaveToFile(saveFilename);
            }
            else if (choice == "4")
            {
                Console.Write("Enter filename to load: ");
                string loadFilename = Console.ReadLine();
                journal.LoadFromFile(loadFilename);
            }
            else if (choice == "5")
            {
                return; // Exit the program
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}

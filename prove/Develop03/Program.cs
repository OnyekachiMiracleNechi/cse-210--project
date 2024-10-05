using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Represents a reference to the scripture
    public class Reference
    {
        public string Book { get; }
        public int StartChapter { get; }
        public int StartVerse { get; }
        public int? EndVerse { get; }

        public Reference(string book, int startChapter, int startVerse, int? endVerse = null)
        {
            Book = book;
            StartChapter = startChapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public override string ToString()
        {
            return EndVerse.HasValue ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}" : $"{Book} {StartChapter}:{StartVerse}";
        }
    }

    // Represents a scripture with its text and reference
    public class Scripture
    {
        private List<Word> words; // List of words in the scripture
        public Reference Ref { get; } // The reference (e.g., John 3:16)

        public Scripture(Reference reference, string text)
        {
            Ref = reference;
            // Split the scripture text into individual words
            words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        // Hide a specified number of random words from the scripture
        public void HideRandomWords(int count)
        {
            var hiddenWords = words.Where(w => !w.IsHidden).OrderBy(x => Guid.NewGuid()).Take(count).ToList();
            foreach (var word in hiddenWords)
            {
                word.Hide();
            }
        }

        // Display the scripture, showing hidden words as underscores
        public string Display()
        {
            return $"{Ref}\n" + string.Join(" ", words.Select(w => w.IsHidden ? "_____" : w.Text));
        }

        // Check if all words are hidden
        public bool AllHidden => words.All(w => w.IsHidden);

        // Get count of visible words
        public int VisibleWordCount => words.Count(w => !w.IsHidden);
    }

    // Represents a single word in the scripture
    public class Word
    {
        public string Text { get; } // The actual word text
        public bool IsHidden { get; private set; } // Whether the word is hidden

        public Word(string text)
        {
            Text = text;
            IsHidden = false; // Initialize as not hidden
        }

        // Method to hide the word
        public void Hide()
        {
            IsHidden = true;
        }
    }

    // Represents a library of scriptures
    public class ScriptureLibrary
    {
        private List<Scripture> scriptures; // Collection of scriptures

        public ScriptureLibrary()
        {
            // Initializing the library with a few scriptures
            scriptures = new List<Scripture>
            {
                new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
                new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
                new Scripture(new Reference("Philippians", 4, 13), "I can do all things through Christ who strengthens me.")
            };
        }

        // Retrieve a scripture by index
        public Scripture GetScripture(int index)
        {
            if (index >= 0 && index < scriptures.Count)
                return scriptures[index];
            else
                return null; 
        }

        // Display the available scriptures for user selection
        public void DisplayAvailableScriptures()
        {
            for (int i = 0; i < scriptures.Count; i++)
            {  
                // List each scripture with its index
                Console.WriteLine($"{i + 1}: {scriptures[i].Ref}"); 
            }
        }

        // Get the count of scriptures available
        public int GetScriptureCount() => scriptures.Count;
    }

    // Main program class
    class Program
    {
        static void Main(string[] args)
        {
             // Create a new scripture library
             // Track total hidden words
            ScriptureLibrary library = new ScriptureLibrary();
            int totalHidden = 0; 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose a scripture to memorize:");
                // Show the available scriptures to the user
                library.DisplayAvailableScriptures(); 
                Console.WriteLine("\nEnter the number of the scripture you want to view (or type 'quit' to exit):");

                var input = Console.ReadLine();
                if (input?.ToLower() == "quit")
               
                // Exit the program if the user types "quit"
                {
                    break; 
                }

                // Validate user input for scripture selection
                if (int.TryParse(input, out var index) && index > 0 && index <= library.GetScriptureCount())
                {
                    // Get the selected scripture
                    var scripture = library.GetScripture(index - 1); 
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine(scripture.Display()); 
                        Console.WriteLine("Press Enter to hide some words or type 'quit' to exit:");

                        var hideInput = Console.ReadLine();
                        if (hideInput?.ToLower() == "quit")
                        {
                            return; 
                        }

                        Console.WriteLine("How many words would you like to hide? (Enter a number):");
                        int wordsToHide;

                        // Try parsing the input, default to 2 if invalid
                        if (!int.TryParse(Console.ReadLine(), out wordsToHide))
                        {
                            wordsToHide = 2; 
                        }

                        // Validate the number of words to hide
                        if (wordsToHide > scripture.VisibleWordCount)
                        {
                            Console.WriteLine($"Invalid number. You can only hide up to {scripture.VisibleWordCount} visible words.");
                            Console.WriteLine("Press Enter to try again or type 'quit' to exit.");
                           // Exit if user chooses to quit
                            if (Console.ReadLine()?.ToLower() == "quit") return; 
                            continue; 
                        }

                        scripture.HideRandomWords(wordsToHide); // Hide the specified number of words
                        totalHidden += wordsToHide; // Update total hidden words

                        // Check if all words are hidden
                        if (scripture.AllHidden)
                        {
                            Console.Clear();
                            Console.WriteLine("All words are now hidden. You can choose another scripture to memorize.");
                            Console.WriteLine($"Total words memorized: {totalHidden}"); 
                            Console.WriteLine("Press Enter to continue to choose another scripture.");
                            Console.ReadLine(); 
                            break; 
                        }

                        // Show current total memorized words
                        Console.WriteLine($"Total words memorized: {totalHidden}"); 
                    }
                }
                else
                {
                    // Prompt for valid input
                    Console.WriteLine("Invalid input. Please try again."); 
                }
            }
        }
    }
}

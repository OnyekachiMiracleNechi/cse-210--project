using System;
using System.Collections.Generic;
using System.Threading;


/* Added creativity by creating more commands like exercise, drink, and meditate and adding a prompt that
displays the calculating process*/


// This is the base class for all types of goals.
abstract class Goal
{
    protected string Name;        // The name of the goal
    protected string Description; // A description of what the goal is about
    protected int Points;         // The points you earn for completing the goal

    public Goal(string name, string description, int points)
    {
        Name = name; // Set the name of the goal
        Description = description; // Set the description of the goal
        Points = points; // Set the points for this goal
    }

    // This method records when you achieve the goal.
    public abstract int RecordEvent();

    // This method gives details about the goal.
    public abstract string GetDetails();
}

// This class represents a goal that can be completed just once.
class SimpleGoal : Goal
{
    private bool IsComplete; // Keeps track of whether this goal is done

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        IsComplete = false; // Start with the goal not being complete
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            IsComplete = true; // Mark the goal as complete
            Console.WriteLine($"🎉 Hooray! You've conquered the goal: {Name}! What a champion! 🎉");
            return Points; // Return the points you earned for completing the goal
        }
        Console.WriteLine($"🚫 Oh no! You've already achieved the glorious feat of completing: {Name}!");
        return 0; // No points if the goal is already complete
    }

    public override string GetDetails()
    {
        // Show whether the goal is complete or not
        return IsComplete ? $"[X] {Name} - {Description} (Completed)" : $"[ ] {Name} - {Description}";
    }
}

// This class is for goals that can be recorded multiple times.
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        Console.WriteLine($"✨ Brilliant! You've scored {Points} points for {Name}! Keep shining! ✨");
        return Points; // Return the points earned for each time you achieve this goal
    }

    public override string GetDetails()
    {
        // Just show the name and description for this goal
        return $"{Name} - {Description}";
    }
}

// This class represents a goal that requires multiple completions.
class ChecklistGoal : Goal
{
    private int TimesCompleted; // How many times you've completed this goal
    private int TimesRequired;   // How many times you need to complete it

    public ChecklistGoal(string name, string description, int points, int timesRequired)
        : base(name, description, points)
    {
        TimesRequired = timesRequired; // Set how many times you need to complete it
        TimesCompleted = 0; // Start with no completions
    }

    public override int RecordEvent()
    {
        TimesCompleted++; // Increment the completion count
        int score = Points; // Start with the base points for this goal
        if (TimesCompleted == TimesRequired)
        {
            score += 500; // Add bonus points for completing the checklist
            Console.WriteLine($"🏆 Wow! You did it! You've completed your checklist goal: {Name}! You earned a fabulous bonus of 500 points! 🏆");
        }
        else
        {
            Console.WriteLine($"✅ Keep going! You've completed this goal {TimesCompleted}/{TimesRequired} times! You're on fire! 🔥");
        }
        return score; // Return the points earned for this event
    }

    public override string GetDetails()
    {
        // Show how many times you've completed this goal
        return $"{Name} - {Description} (Completed {TimesCompleted}/{TimesRequired} times)";
    }
}

// This class manages all your goals and tracks your total score.
class GoalManager
{
    private List<Goal> goals = new List<Goal>(); // A list to hold all your goals
    private int totalScore; // Your total score

    public int TotalScore => totalScore; // A way to access your total score

    // This method adds a new goal to your list.
    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    // This method records an event for a specific goal and updates your score.
    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            totalScore += goals[goalIndex].RecordEvent(); // Add the points from the goal
        }
    }

    // This method displays all your goals and your current score.
    public void ShowGoals()
    {
        Console.WriteLine("\n🎯 Your Goals:");
        foreach (var goal in goals)
        {
            Console.WriteLine(goal.GetDetails()); // Show each goal's details
        }
        Console.WriteLine($"🌟 Total Score: {TotalScore}"); // Show your total score
    }

    // This method adds points to your total score.
    public void AddToScore(int points)
    {
        totalScore += points;
    }

    // This method displays your total score.
    public void ShowTotalScore()
    {
        Console.WriteLine($"🌟 Your total score is: {TotalScore} points! Keep up the great work! 🌟");
    }
}

// The main program where everything happens.
class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager(); // Create a new goal manager
        int totalPointsEarned = 0; // Keep track of points earned during the session

        // Add some predefined goals to start with
        goalManager.AddGoal(new SimpleGoal("Run a Marathon", "Complete a full marathon", 1000));
        goalManager.AddGoal(new EternalGoal("Read Scriptures", "Read scriptures daily", 100));
        goalManager.AddGoal(new ChecklistGoal("Attend Temple", "Attend the temple 10 times", 50, 10));

        // This loop keeps the program running, waiting for user input
        while (true)
        {
            Console.WriteLine("\n✨ Welcome to your Eternal Quest! What exciting adventure would you like to embark on today?");
            // Show available commands
            Console.WriteLine("Commands:");
            Console.WriteLine("run - Earn points for running");
            Console.WriteLine("read - Earn points for reading scriptures");
            Console.WriteLine("attend - Record attending the temple");
            Console.WriteLine("meditate - Earn points for meditation");
            Console.WriteLine("exercise - Earn points for exercising");
            Console.WriteLine("drink - Log your water intake");
            Console.WriteLine("calculate - Calculate your recent activities");
            Console.WriteLine("show - Show goals and score");
            Console.WriteLine("exit - Exit the program");
            Console.Write("Please select a command: ");

            string command = Console.ReadLine()?.ToLower(); // Get the user's command

            // Handle different commands based on user input
            switch (command)
            {
                case "run":
                    // Ask the user how they want to track their run
                    Console.WriteLine("🏃‍♂️ Ready to sprint into action? How do you want to track your run: seconds or minutes? (Enter 's' for seconds or 'm' for minutes)");
                    string runTimeUnit = Console.ReadLine()?.ToLower();
                    int runTime = 0;

                    // If the user chooses minutes
                    if (runTimeUnit == "m")
                    {
                        Console.WriteLine("How many minutes will you conquer today? Each minute gives you 10 points!");
                        if (int.TryParse(Console.ReadLine(), out int runMinutes) && runMinutes > 0)
                        {
                            runTime = runMinutes * 60; // Convert minutes to seconds
                        }
                        else
                        {
                            Console.WriteLine("❌ That's not a valid number! Let's skip the running for now.");
                            break;
                        }
                    }
                    // If the user chooses seconds
                    else if (runTimeUnit == "s")
                    {
                        Console.WriteLine("How many seconds will you push through? Each 10 seconds gives you 1 point!");
                        if (int.TryParse(Console.ReadLine(), out int runSeconds) && runSeconds > 0)
                        {
                            runTime = runSeconds; // Use the input seconds directly
                        }
                        else
                        {
                            Console.WriteLine("❌ That's not a valid number! Let's skip the running for now.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid time unit! Let's skip the running for now.");
                        break;
                    }

                    // Show a message while calculating points
                    Console.WriteLine("⏳ Calculating your running points...");
                    Thread.Sleep(1000);
                    Console.WriteLine($"⏳ Running for {runTime / 60} minute(s)...");

                    // Countdown to simulate the running time
                    for (int i = runTime; i > 0; i--)
                    {
                        Console.WriteLine($"⏳ {i / 60} minute(s) and {i % 60} second(s) remaining...");
                        Thread.Sleep(1000); // Wait for one second
                    }

                    int pointsEarnedRun = (runTime / 10); // Calculate the points earned for running
                    goalManager.RecordEvent(0); // Record the event for the SimpleGoal
                    goalManager.AddToScore(pointsEarnedRun); // Add the points to the total score
                    totalPointsEarned += pointsEarnedRun; // Update the total points
                    Console.WriteLine($"✨ You've earned {pointsEarnedRun} points for your epic run! Keep it up! 🏃‍♀️✨");
                    break;

                case "read":
                    // Ask how the user wants to track their scripture reading
                    Console.WriteLine("📖 How do you want to track your scripture reading: number of scriptures or time spent (in seconds)? (Enter 'n' for number of scriptures or 's' for seconds)");
                    string scriptureUnit = Console.ReadLine()?.ToLower();

                    // If the user chooses number of scriptures
                    if (scriptureUnit == "n")
                    {
                        Console.WriteLine("How many scriptures did you read? Each scripture gives you 10 points!");
                        if (int.TryParse(Console.ReadLine(), out int scriptureCount) && scriptureCount > 0)
                        {
                            Console.WriteLine("⏳ Calculating your scripture points...");
                            Thread.Sleep(1000); // Simulate calculation time
                            int pointsEarnedScripture = scriptureCount * 10; // Calculate points earned
                            goalManager.RecordEvent(1); // Record event for the EternalGoal
                            goalManager.AddToScore(pointsEarnedScripture); // Add points to total score
                            totalPointsEarned += pointsEarnedScripture; // Update total points
                            Console.WriteLine($"✨ You've earned {pointsEarnedScripture} points for reading {scriptureCount} scripture(s)! 📖✨");
                        }
                        else
                        {
                            Console.WriteLine("❌ That's not a valid number! Let's skip the scripture reading for now.");
                        }
                    }
                    // If the user chooses time spent reading
                    else if (scriptureUnit == "s")
                    {
                        Console.WriteLine("How many seconds did you spend reading? Each 10 seconds gives you 1 point!");
                        if (int.TryParse(Console.ReadLine(), out int scriptureSeconds) && scriptureSeconds > 0)
                        {
                            Console.WriteLine("⏳ Calculating your scripture points...");
                            Thread.Sleep(1000); // Simulate calculation time
                            int pointsEarnedScripture = scriptureSeconds / 10; // Calculate points earned
                            goalManager.RecordEvent(1); // Record event for the EternalGoal
                            goalManager.AddToScore(pointsEarnedScripture); // Add points to total score
                            totalPointsEarned += pointsEarnedScripture; // Update total points
                            Console.WriteLine($"✨ You've earned {pointsEarnedScripture} points for reading for {scriptureSeconds} second(s)! 📖✨");
                        }
                        else
                        {
                            Console.WriteLine("❌ That's not a valid number! Let's skip the scripture reading for now.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid input! Let's skip the scripture reading for now.");
                    }
                    break;

                case "attend":
                    // Log temple attendance
                    Console.WriteLine("🏛️ How many times have you attended the temple? Each visit gives you 50 points!");
                    if (int.TryParse(Console.ReadLine(), out int attendCount) && attendCount > 0)
                    {
                        Console.WriteLine("⏳ Calculating your temple attendance points...");
                        Thread.Sleep(1000); // Simulate calculation time
                        for (int i = attendCount; i > 0; i--)
                        {
                            Console.WriteLine($"⏳ Attending the temple for the {i} time...");
                            Thread.Sleep(1000); // Countdown for each attendance
                        }
                        int pointsEarnedAttend = attendCount * 50; // Calculate points earned for attendance
                        goalManager.RecordEvent(2); // Record event for the ChecklistGoal
                        goalManager.AddToScore(pointsEarnedAttend); // Add points to total score
                        totalPointsEarned += pointsEarnedAttend; // Update total points
                        Console.WriteLine($"✨ You've earned {pointsEarnedAttend} points for attending the temple {attendCount} time(s)! 🏛️✨");
                    }
                    else
                    {
                        Console.WriteLine("❌ That's not a valid number! Let's skip the temple attendance for now.");
                    }
                    break;

                case "meditate":
                    // Track meditation time
                    Console.WriteLine("🧘‍♂️ How many minutes will you meditate? Each minute gives you 20 points!");
                    if (int.TryParse(Console.ReadLine(), out int meditateMinutes) && meditateMinutes > 0)
                    {
                        Console.WriteLine("⏳ Calculating your meditation points...");
                        Thread.Sleep(1000); // Simulate calculation time
                        for (int i = meditateMinutes; i > 0; i--)
                        {
                            Console.WriteLine($"⏳ Meditating for {i} minute(s)...");
                            Thread.Sleep(1000); // Countdown for each minute
                        }
                        int pointsEarnedMeditate = meditateMinutes * 20; // Calculate points earned for meditation
                        goalManager.AddToScore(pointsEarnedMeditate); // Add points to total score
                        totalPointsEarned += pointsEarnedMeditate; // Update total points
                        Console.WriteLine($"✨ You've earned {pointsEarnedMeditate} points for meditating for {meditateMinutes} minute(s)! 🧘‍♀️✨");
                    }
                    else
                    {
                        Console.WriteLine("❌ That's not a valid number! Let's skip the meditation for now.");
                    }
                    break;

                case "exercise":
                    // Track exercise time
                    Console.WriteLine("💪 How many minutes did you exercise today? Each minute gives you 15 points!");
                    if (int.TryParse(Console.ReadLine(), out int exerciseMinutes) && exerciseMinutes > 0)
                    {
                        Console.WriteLine("⏳ Calculating your exercise points...");
                        Thread.Sleep(1000); // Simulate calculation time
                        for (int i = exerciseMinutes; i > 0; i--)
                        {
                            Console.WriteLine($"⏳ Exercising for {i} minute(s)...");
                            Thread.Sleep(1000); // Countdown for each minute
                        }
                        int pointsEarnedExercise = exerciseMinutes * 15; // Calculate points earned for exercise
                        goalManager.AddToScore(pointsEarnedExercise); // Add points to total score
                        totalPointsEarned += pointsEarnedExercise; // Update total points
                        Console.WriteLine($"✨ You've earned {pointsEarnedExercise} points for exercising for {exerciseMinutes} minute(s)! 💪✨");
                    }
                    else
                    {
                        Console.WriteLine("❌ That's not a valid number! Let's skip the exercise for now.");
                    }
                    break;

                case "drink":
                    // Log water intake
                    Console.WriteLine("💧 How many glasses of water will you drink? Each glass gives you 5 points!");
                    if (int.TryParse(Console.ReadLine(), out int glassesCount) && glassesCount > 0)
                    {
                        Console.WriteLine("⏳ Calculating your water intake points...");
                        Thread.Sleep(1000); // Simulate calculation time
                        int pointsEarnedDrink = glassesCount * 5; // Calculate points earned for drinking water
                        goalManager.AddToScore(pointsEarnedDrink); // Add points to total score
                        totalPointsEarned += pointsEarnedDrink; // Update total points
                        Console.WriteLine($"✨ You've earned {pointsEarnedDrink} points for drinking {glassesCount} glass(es) of water! 💧✨");
                    }
                    else
                    {
                        Console.WriteLine("❌ That's not a valid number! Let's skip logging your water intake for now.");
                    }
                    break;

                case "calculate":
                    // Show the total points earned during the session
                    Console.WriteLine("🔍 Calculating your total score based on your inputs...");
                    Thread.Sleep(1000); // Simulate calculation time
                    goalManager.ShowTotalScore(); // Show the total score
                    break;

                case "show":
                    // Display the goals and the current total score
                    goalManager.ShowGoals();
                    break;

                case "exit":
                    Console.WriteLine("🚪 Thank you for playing! See you next time! 🌈");
                    return; // Exit the program

                default:
                    Console.WriteLine("❓ Hmm, I didn't recognize that command. Please try again!"); // Handle unrecognized commands
                    break;
            }
        }
    }
}

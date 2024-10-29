using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



/*showed creativity by  Improved menu display for a better user experience.
Added a method to show goal statistics, providing insight into user progress.

*/

namespace GoalTrackingApp
{
    // Base class for all goals
    public abstract class Goal
    {
        protected string _name;
        protected string _description;
        protected int _points;
        protected bool _isComplete;

        public Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
            _isComplete = false;
        }

        public abstract int RecordEvent();
        public abstract string GetDetails();

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;
        public bool IsComplete => _isComplete;

        public void Complete() => _isComplete = true; // Mark as complete
    }

    // Simple Goal class
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                Console.WriteLine($"🎉 You completed: {_name}!");
                return _points;
            }
            Console.WriteLine($"🚫 You've already completed: {_name}!");
            return 0;
        }

        public override string GetDetails()
        {
            return $"{_name} - {_description} (Points: {_points}) [Completed: {_isComplete}]";
        }
    }

    // Eternal Goal class
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override int RecordEvent()
        {
            Console.WriteLine($"✨ You earned {_points} points for {_name}!");
            return _points;
        }

        public override string GetDetails()
        {
            return $"{_name} - {_description} (Points: {_points}) [Eternal Goal]";
        }
    }

    // Checklist Goal class
    public class ChecklistGoal : Goal
    {
        private int _timesCompleted;
        private int _timesRequired;

        public ChecklistGoal(string name, string description, int points, int timesRequired)
            : base(name, description, points)
        {
            _timesRequired = timesRequired;
            _timesCompleted = 0;
        }

        public override int RecordEvent()
        {
            if (_timesCompleted < _timesRequired)
            {
                _timesCompleted++;
                Console.WriteLine($"✅ Progress for {_name}: {_timesCompleted}/{_timesRequired}");
                if (_timesCompleted == _timesRequired)
                {
                    _isComplete = true;
                    Console.WriteLine($"🎉 You've completed the checklist goal: {_name}!");
                    return _points + 500; // Bonus for completion
                }
                return _points;
            }
            Console.WriteLine($"🚫 You've already completed the checklist goal: {_name}!");
            return 0;
        }

        public override string GetDetails()
        {
            return $"{_name} - {_description} (Points: {_points}) [Completed: {_isComplete}] (Progress: {_timesCompleted}/{_timesRequired})";
        }

        public void SetTimesCompleted(int times) => _timesCompleted = times; // Set progress
        public int TimesCompleted => _timesCompleted; // Property for accessing timesCompleted
        public int TimesRequired => _timesRequired; // Property for accessing timesRequired
    }

    // Goal manager to handle multiple goals
    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _totalPoints;

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void ShowGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals have been added yet.");
                return;
            }

            Console.WriteLine("Your Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
            }
        }

        public void RecordGoalEvent(int index)
        {
            if (index >= 0 && index < _goals.Count)
            {
                _totalPoints += _goals[index].RecordEvent();
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }

        public void SaveGoals(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var goal in _goals)
                {
                    string completionStatus = goal.IsComplete.ToString();
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Description},{goal.Points},{checklistGoal.TimesRequired},{checklistGoal.TimesCompleted},{completionStatus}");
                    }
                    else
                    {
                        writer.WriteLine($"{goal.GetType().Name},{goal.Name},{goal.Description},{goal.Points},{completionStatus}");
                    }
                }
                writer.WriteLine($"TotalPoints,{_totalPoints}");
            }
        }

        public void LoadGoals(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    try
                    {
                        if (parts[0] == "SimpleGoal")
                        {
                            AddGoal(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3])));
                        }
                        else if (parts[0] == "EternalGoal")
                        {
                            AddGoal(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                        }
                        else if (parts[0] == "ChecklistGoal")
                        {
                            var checklistGoal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
                            checklistGoal.SetTimesCompleted(int.Parse(parts[5]));
                            if (bool.TryParse(parts[6], out bool completeStatus) && completeStatus)
                            {
                                checklistGoal.Complete(); // Mark as complete if applicable
                            }
                            AddGoal(checklistGoal);
                        }
                        else if (parts[0] == "TotalPoints")
                        {
                            _totalPoints = int.Parse(parts[1]);
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing line: {line}. Exception: {ex.Message}");
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine($"Error: line format is incorrect: {line}. Exception: {ex.Message}");
                    }
                }
            }
        }

        public void ShowStatistics()
        {
            int totalGoals = _goals.Count;
            int completedGoals = _goals.Count(g => g.IsComplete);
            int totalPoints = _totalPoints;

            Console.WriteLine("\nGoal Statistics:");
            Console.WriteLine($"Total Goals: {totalGoals}");
            Console.WriteLine($"Completed Goals: {completedGoals}");
            Console.WriteLine($"Total Points Earned: {totalPoints}");
            Console.WriteLine($"Completion Rate: {(totalGoals > 0 ? (completedGoals * 100 / totalGoals) : 0)}%");
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            manager.LoadGoals("goals.txt"); // Load existing goals

            while (true)
            {
                Console.WriteLine("\n====================");
                Console.WriteLine(" Welcome to Goal Tracker ");
                Console.WriteLine("====================");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Simple Goal");
                Console.WriteLine("2. Add Eternal Goal");
                Console.WriteLine("3. Add Checklist Goal");
                Console.WriteLine("4. Show Goals");
                Console.WriteLine("5. Record Goal Event");
                Console.WriteLine("6. Save Goals");
                Console.WriteLine("7. Show Statistics");
                Console.WriteLine("8. Exit");

                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Add Simple Goal
                        Console.Write("Enter goal name: ");
                        string simpleGoalName = Console.ReadLine();
                        Console.Write("Enter goal description: ");
                        string simpleGoalDescription = Console.ReadLine();
                        Console.Write("Enter goal points: ");
                        if (int.TryParse(Console.ReadLine(), out int simpleGoalPoints))
                        {
                            manager.AddGoal(new SimpleGoal(simpleGoalName, simpleGoalDescription, simpleGoalPoints));
                            Console.WriteLine("Simple goal added.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid points. Please enter a number.");
                        }
                        break;

                    case "2":
                        // Add Eternal Goal
                        Console.Write("Enter goal name: ");
                        string eternalGoalName = Console.ReadLine();
                        Console.Write("Enter goal description: ");
                        string eternalGoalDescription = Console.ReadLine();
                        Console.Write("Enter goal points: ");
                        if (int.TryParse(Console.ReadLine(), out int eternalGoalPoints))
                        {
                            manager.AddGoal(new EternalGoal(eternalGoalName, eternalGoalDescription, eternalGoalPoints));
                            Console.WriteLine("Eternal goal added.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid points. Please enter a number.");
                        }
                        break;

                    case "3":
                        // Add Checklist Goal
                        Console.Write("Enter goal name: ");
                        string checklistGoalName = Console.ReadLine();
                        Console.Write("Enter goal description: ");
                        string checklistGoalDescription = Console.ReadLine();
                        Console.Write("Enter goal points: ");
                        if (int.TryParse(Console.ReadLine(), out int checklistGoalPoints))
                        {
                            Console.Write("Enter number of times required to complete: ");
                            if (int.TryParse(Console.ReadLine(), out int timesRequired))
                            {
                                manager.AddGoal(new ChecklistGoal(checklistGoalName, checklistGoalDescription, checklistGoalPoints, timesRequired));
                                Console.WriteLine("Checklist goal added.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of times required. Please enter a number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid points. Please enter a number.");
                        }
                        break;

                    case "4":
                        manager.ShowGoals();
                        break;

                    case "5":
                        Console.Write("Enter goal index to record: ");
                        if (int.TryParse(Console.ReadLine(), out int index))
                        {
                            manager.RecordGoalEvent(index - 1); // Adjust for zero-based index
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                        }
                        break;

                    case "6":
                        manager.SaveGoals("goals.txt");
                        Console.WriteLine("Goals saved successfully.");
                        break;

                    case "7":
                        manager.ShowStatistics();
                        break;

                    case "8":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
    }
}

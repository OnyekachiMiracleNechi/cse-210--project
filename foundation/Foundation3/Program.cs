using System;
using System.Collections.Generic;

// Base class representing a generic Activity
public class Activity
{
    // Private fields for the date and duration (in minutes) of the activity
    private string _date;
    private int _minutes;

    // Constructor to initialize common attributes for all activities
    public Activity(string date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Public properties to access the date and duration
    public string Date => _date;
    public int Minutes => _minutes;

    // Virtual methods for calculating distance, speed, and pace - to be overridden by derived classes
    public virtual double GetDistance() => 0;
    public virtual double GetSpeed() => 0;
    public virtual double GetPace() => 0;

    // Virtual method to get a summary of the activity - can be overridden in derived classes if needed
    public virtual string GetSummary()
    {
        return $"{Date} Activity ({Minutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace {GetPace():0.0} min per km";
    }
}

// Derived class for Running activity
public class Running : Activity
{
    // Private field to store the distance in kilometers
    private double _distance;

    // Constructor to initialize date, duration, and distance for running
    public Running(string date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    // Override methods to calculate specific values for running
    public override double GetDistance() => _distance;
    public override double GetSpeed() => (GetDistance() / Minutes) * 60; // Speed in kph
    public override double GetPace() => Minutes / GetDistance(); // Pace in min per km

    // Override GetSummary to provide a formatted output specific to running
    public override string GetSummary()
    {
        return $"{Date} Running ({Minutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace {GetPace():0.0} min per km";
    }
}

// Derived class for Cycling activity
public class Cycling : Activity
{
    // Private field to store speed in kilometers per hour (kph)
    private double _speed;

    // Constructor to initialize date, duration, and speed for cycling
    public Cycling(string date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    // Override methods to calculate specific values for cycling
    public override double GetDistance() => (_speed * Minutes) / 60; // Distance in km
    public override double GetSpeed() => _speed; // Speed as initialized
    public override double GetPace() => 60 / _speed; // Pace in min per km

    // Override GetSummary to provide a formatted output specific to cycling
    public override string GetSummary()
    {
        return $"{Date} Cycling ({Minutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace {GetPace():0.0} min per km";
    }
}

// Derived class for Swimming activity
public class Swimming : Activity
{
    // Private field to store the number of laps
    private int _laps;

    // Constructor to initialize date, duration, and laps for swimming
    public Swimming(string date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    // Override methods to calculate specific values for swimming
    public override double GetDistance() => _laps * 50 / 1000.0; // Distance in km (each lap is 50 meters)
    public override double GetSpeed() => (GetDistance() / Minutes) * 60; // Speed in kph
    public override double GetPace() => Minutes / GetDistance(); // Pace in min per km

    // Override GetSummary to provide a formatted output specific to swimming
    public override string GetSummary()
    {
        return $"{Date} Swimming ({Minutes} min): Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace {GetPace():0.0} min per km";
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create a list to hold different activities
        List<Activity> activities = new List<Activity>
        {
            new Running("03 Nov 2022", 30, 4.8), // Running activity
            new Cycling("03 Nov 2022", 30, 20.0), // Cycling activity
            new Swimming("03 Nov 2022", 30, 20) // Swimming activity with number of laps
        };

        // Iterate through each activity and display its summary
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

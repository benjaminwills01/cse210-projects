using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks.Sources;

//
public abstract class Goal
{

    protected string name;
    protected int points;
    protected bool isCompleted;

    public string Name { get; set; }

    public int Points { get; protected set; }

    public Goal(string name, int points)
    {
        this.Name = name;
        this.points = points;
        this.isCompleted = false;
    }

    public virtual void MarkComplete()
    {
        isCompleted = true;

        AddPoints();
        OnComplete();
    }

    protected virtual void AddPoints()
    {
        Points += points;
    }

    protected abstract void OnComplete();

    public string GetStatus()
    {
        string status = isCompleted ? "[X]" : "[ ]";
        return $"{status} {name}";
    }

    public virtual void Display()
    {
        Console.WriteLine("Goal: " + name);
    }

    public abstract string GetStringRepresentation();
}



//
public class SimpleGoal : Goal
{

    private DateTime deadline;

    public SimpleGoal(string name, DateTime deadline) : base(name, 0)
    {
        this.deadline = deadline;
    }

    public override void Display()
    {
        Console.WriteLine("Simple Goal: " + name + ", Deadline: " + deadline.ToShortDateString());
    }

    protected override void AddPoints()
    {
        Points += 1000;
    }

    protected override void OnComplete()
    {
        
        Console.WriteLine("Simple Goal Completed: " + name);
    }

    public override string GetStringRepresentation()
    {

        return name;
    }
}



//
public class EternalGoal : Goal
{

    public EternalGoal(string name) : base(name, 0)
    {

    }

    public override void Display()
    {
        Console.WriteLine("Eternal Goal : " + name + ", This goal has no end.");
    }

    protected override void OnComplete()
    {
        Console.WriteLine("Eternal Goal Completed: " + name);
    }

    public override string GetStringRepresentation()
    {
        return name;
    }
}



//
public class ChecklistGoal : Goal
{

    private List<string> subgoals;

    public ChecklistGoal(string name, List<string> subgoals) : base(name, 0)

    {
        this.subgoals = subgoals;
    }

    public override void Display()
    {
        Console.WriteLine($"Checklist Goal: {name}, Subgoals:");
        foreach (string subgoal in subgoals)
        {
            Console.WriteLine($"- {subgoal}");
        }
    }

    protected override void OnComplete()
    {
       Console.WriteLine("Checklist Goal Completed: " + name); 
    }

    public override string GetStringRepresentation()
    {
    
        return name;
    }
}



//
public class GoalTracking
{   
    
    private List<Goal> goals;

    public GoalTracking()
    {
        goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
        Console.WriteLine("Goal added: " + goal.GetStatus());
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Current Goals:");
        foreach (Goal goal in goals)
        {
            goal.Display();
        }
    }

    public void SaveGoals(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Goal goal in goals)
            {
                string goalString = SerializeGoal(goal);
                writer.WriteLine(goalString);
            }
        }
        Console.WriteLine("Goals saved to file: " + filePath);
    }

    public void LoadGoals(string filePath)
    {
        goals.Clear();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Goal goal = DeserializeGoal(line);
                if (goal != null)
                {
                    goals.Add(goal);
                }
            }
        }
        Console.WriteLine("Goals loaded from file: " + filePath);
    }

    private string SerializeGoal(Goal goal)
    {
        string goalType = goal.GetType().Name;
        string goalDetails = goal.GetStringRepresentation();
        return $"{goalType}:{goalDetails}";
    }

    public Goal FindGoalByName(string name)
    {

        foreach (Goal goal in goals)
        {
            if (goal.Name.Equals(name, StringComparison.OrdinalIgnoreCase ))
            {
                return goal;
            }
        }
        return null;
    }

    private Goal DeserializeGoal(string goalString)
    {
        string[] parts = goalString.Split(':');
        if (parts.Length >= 2)
        {
            string goalType = parts[0];
            string goalDetails = parts[1];
            switch (goalType)
            {
                case "SimpleGoal":
                    string[] simpleGoalDetails = goalDetails.Split(';');
                    if (simpleGoalDetails.Length == 2 && DateTime.TryParse(simpleGoalDetails[1], out DateTime deadline))
                    {
                        return new SimpleGoal(simpleGoalDetails[0], deadline);
                    }
                    else
                    {
                        Console.WriteLine("Invalid SimpleGoal details: " + goalDetails);
                        return null;
                    }

                case "EternalGoal":
                    return new EternalGoal(goalDetails);

                case "ChecklistGoal":
                    string[] checklistSubgoals = goalDetails.Split(';');
                    List<string> subgoals = new List<string>(checklistSubgoals);
                    return new ChecklistGoal(subgoals[0], subgoals.GetRange(1, subgoals.Count - 1));

                default:
                    Console.WriteLine("Unknown goal type: " + goalType);
                    return null;
            }
        }
        else
        {
            Console.WriteLine("Invalid goal string format: " + goalString);
            return null;
        }
    }
}



//
class Program
{
    static void Main(string[] args)
    {
        GoalTracking goalApp = new GoalTracking();

        while (true)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.WriteLine("Select a choice from the Menu:");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                Console.WriteLine("Enter the type of goal (Simple, Eternal, or Checklist):");
                string goalType = Console.ReadLine();

                Console.WriteLine("Enter the name of the goal:");
                string goalName = Console.ReadLine();

                switch (goalType.ToLower())
                {
                    case "simple":
                        Console.WriteLine("Enter the deadline (YYYY-MM-DD) for the goal:");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
                        {
                            goalApp.AddGoal(new SimpleGoal(goalName, deadline));
                            Console.WriteLine("Simple Goal added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid deadline format. Simple Goal creation failed.");
                        }
                        break;

                    case "eternal":
                        goalApp.AddGoal(new EternalGoal(goalName));
                        Console.WriteLine("Eternal Goal added successfully!");
                        break;

                    case "checklist":
                        Console.WriteLine("Enter subgoals (separated by comma) for the goal:");
                        string[] subgoals = Console.ReadLine().Split(',');
                        goalApp.AddGoal(new ChecklistGoal(goalName, subgoals.ToList()));
                        Console.WriteLine("Checklist Goal added successfully!");
                        break;

                    default:
                        Console.WriteLine("Invalid goal type. Please enter Simple, Eternal, or Checklist.");
                        break;
                }
                    break;

                case "2":
                    goalApp.DisplayGoals();
                    break;

                case "3":
                    goalApp.SaveGoals("goals.txt");
                    break;

                case "4":
                    goalApp.LoadGoals("goals.txt");
                    break;

                case "5":
                    Console.WriteLine("Enter the name of the goal you have completed:");
                    string completedGoalName = Console.ReadLine();

                    Goal completedGoal = goalApp.FindGoalByName(completedGoalName);

                    if (completedGoal != null)
                    {
                        completedGoal.MarkComplete();
                        Console.WriteLine($"Event recorded for goal '{completedGoalName}'.");
                    }
                    else
                    {
                        Console.WriteLine($"Goal '{completedGoalName}' not found.");
                    }
                    break;

                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

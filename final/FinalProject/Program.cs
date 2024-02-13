using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

// Define the Task class to represent individual tasks
public class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted { get; set; }


    // Constructor to initialize task properties
    public Task(string name, string description, DateTime dueDate, int priority)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        IsCompleted = false; // The task is incomplete by default
    }
}


// Define a TaskCategory class to categorize tasks
public class TaskCategory
{
    // Add Code
}


// Define a TaskSorter class to sort tasks based on due date or priority
public class TaskSorter
{
    // Add Code
}


// Define a User class to represent a task manager user
public class User
{
    // Add Code
}


// Define the TaskManager class to manage tasks
public class TaskManager
{
    private List<Task> tasks; // List to store the tasks

    // Constructor to intialize the task list
    public TaskManager()
    {
        tasks = new List<Task>();
    }

    // Method to add a new task to the list
    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    // Method to mark a task complete
    public void MarkTaskAsCompleted(Task task)
    {
        task.IsCompleted = true;
    }

    // Method to display all the tasks
    public void DisplayTasks()
    {
        foreach (Task task in tasks)
        {
            Console.WriteLine($"Task: {task.Name}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Due Date: {task.DueDate}");
            Console.WriteLine($"Priority: {task.Priority}");
            Console.WriteLine($"Completed: {task.IsCompleted}");
            Console.WriteLine("-------------------------------------");
        }
    }
}


// Define InputValidator class to ensure input from the user meets the required format
public class InputValidator
{
    // Add Code
}


// Define the main class to run the program
class Program
{
    static void Main(string[] args)
    {
        //Create a new instance of the TaskManager class
        TaskManager taskManager = new TaskManager();

        // Example: Add tasks to the task manager
        Task task1 = new Task("Finish off the report", "Complete the final details of the project report", DateTime.Now.AddDays(7), 2);
        Task task2 = new Task("Email Jacob", "Email Jacob about the upcoming meeting", DateTime.Now.AddDays(2), 1);

        taskManager.AddTask(task1);
        taskManager.AddTask(task2);

        // Example: Mark a task as completed
        taskManager.MarkTaskAsCompleted(task2);

        // Example: Display all tasks
        Console.WriteLine("All Tasks:");
        taskManager.DisplayTasks();
    }
}
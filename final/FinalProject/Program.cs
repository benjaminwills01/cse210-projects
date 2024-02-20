using System;
using System.Collections.Generic;

// Define an enum for task priority levels
public enum PriorityLevel
{
    Low,
    Medium,
    High
}

// Define the Task class to represent individual tasks
public class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Priority { get; set; }
    public bool IsCompleted { get; set; }
    public Task(string name, string description, DateTime dueDate, int priority)
    {
        Name = name;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        IsCompleted = false;
    }

    // Method to get a string representation of the task
    public virtual string GetTaskInfo()
    {
        return $"Task: {Name}\nDescription: {Description}\nDue Date: {DueDate}\nPriority: {Priority}\nCompleted: {IsCompleted}\n-------------------------------------";
    }
}

// Define a subclass of Task called WorkTask for special task
public class WorkTask : Task
{
    public string Department { get; set; }

    // Constructor to initialize work task properties
    public WorkTask(string name, string description, DateTime dueDate, PriorityLevel priority, string department)
        : base(name, description, dueDate, (int)priority)
    {
        Department = department;
    }

    // Overide GetTaskInfo to include department
    public override string GetTaskInfo()
    {
        return base.GetTaskInfo() + $"\nDepartment: {Department}\n";
    }
}

// Define a subclass of Task for specialized tasks (PersonalTask)
public class PersonalTask : Task
{
    public string Assignee { get; set; }

    // Constructor to initialize personal task properties
    public PersonalTask(string name, string description, DateTime dueDate, PriorityLevel priority, string assignee)
        : base(name, description, dueDate, (int)priority)
    {
        Assignee = assignee;
    }

    // Override GetTaskInfo method to include assignee information
    public override string GetTaskInfo()
    {
        return base.GetTaskInfo() + $"\nAssignee: {Assignee}\n";
    }
}

// Define a TaskCategory class to categorize tasks
public class TaskCategory
{
    public string CategoryName { get; set; }
    public List<Task> Tasks { get; set; }

    public TaskCategory(string categoryName)
    {
        CategoryName = categoryName;
        Tasks = new List<Task>();
    }
}
// Define a TaskSorter class to sort tasks based on due date or priority
public class TaskSorter
{
    // Method to sort tasks by due date
    public static void SortByDueDate(List<Task> tasks)
    {
        tasks.Sort((task1, task2) => task1.Priority.CompareTo(task2.Priority));
    }
}
// Define a User class to represent a task manager user
public class User
{
    public string UserName { get; set; }
    public TaskManager TaskManager { get; set; }

    public User(string userName)
    {
        UserName = userName;
        TaskManager = new TaskManager();
    }
}
// Define the TaskManager class to manage tasks
public class TaskManager
{
    //private List<Task> tasks;
    public List<Task> Tasks { get; private set; }

    // Constructor to intialize the task list
    public TaskManager()
    {
        Tasks = new List<Task>();
    }

    // Method to add a new task to the list
    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }

    // Method to mark a task complete
    public void MarkTaskAsCompleted(Task task)
    {
        task.IsCompleted = true;
    }

    // Method to display all the tasks
    public void DisplayTasks()
    {
        foreach (Task task in Tasks)
        {
            Console.WriteLine(task.GetTaskInfo());
        }
    }

    // Method to Display tasks in a category
    public void DisplayTasksInCategory(TaskCategory category)
    {
        Console.WriteLine($"Tasks in Category '{category.CategoryName}':");
        foreach (Task task in category.Tasks)
        {
            Console.WriteLine(task.GetTaskInfo());
        }
    }
}
// Define InputValidator class to ensure input from the user meets the required format
public class InputValidator
{
    // Method to validate task name input
    public static bool ValidateTaskName(string name)
    {
        // Check if the name is not empty
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Task name cannot be empty.");
            return false;
        }

        return true;
    }

    // Method to validate task description input
    public static bool ValidateTaskDescription(string description)
    {
        // Check if the description is not empty
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Task description cannot be empty.");
            return false;
        }

        return true;
    }

    // Method to validate due date input
    public static bool ValidateDueDate(string dueDateStr, out DateTime dueDate)
    {
        if (!DateTime.TryParse(dueDateStr, out dueDate))
        {
            Console.WriteLine("Invalid due date format. Please use yyyy-MM-dd format.");
            return false;
        }

        // Check if the due date is in the future
        if (dueDate < DateTime.Today)
        {
            Console.WriteLine("Due date must be in the future.");
            return false;
        }

        return true;
    }

    // Method to validate priority input
    public static bool ValidatePriority(string priorityStr, out PriorityLevel priority)
    {
        if (!Enum.TryParse(priorityStr, true, out priority))
        {
            Console.WriteLine("Invalid priority level. Please use Low, Medium, or High.");
            return false;
        }

        return true;
    }
}

// Define the main class to run the program
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Task Manager!");

        // Create a new instance of the User class
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();
        User user = new User(username);

        TaskManager taskManager = user.TaskManager;

        bool continueManagingTasks = true;
        while (continueManagingTasks)
        {
            Console.WriteLine("\nMenu Options: ");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View your Tasks");
            Console.WriteLine("3. Mark a Task as Completed");
            Console.WriteLine("4. Exit Task Manager");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask(taskManager);
                    break;
                case "2":
                    Console.WriteLine("\nYour Tasks:\n");
                    taskManager.DisplayTasks();
                    break;
                case "3":
                    MarkTaskAsCompleted(taskManager);
                    break;
                case "4":
                    continueManagingTasks = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. ");
                    break;
            }
        }
    }

    // Method to add a task
    static void AddTask(TaskManager taskManager)
    {
        Console.WriteLine("\nPlease enter the following details:");
        Console.Write("Task Name: ");
        string name = Console.ReadLine();
        Console.Write("Task Description: ");
        string description = Console.ReadLine();
        Console.Write("Due Date (yyyy-MM-dd): ");
        DateTime dueDate = DateTime.Parse(Console.ReadLine());
        Console.Write("Priority Level (Low, Medium, High): ");
        PriorityLevel priority = (PriorityLevel)Enum.Parse(typeof(PriorityLevel), Console.ReadLine(), true);

        Task newTask = new Task(name, description, dueDate, (int)priority);

        taskManager.AddTask(newTask);
        Console.WriteLine("Task successfully added! ");
    }

    // Method to mark a task as completed
    static void MarkTaskAsCompleted(TaskManager taskManager)
    {
        Console.Write("\nEnter the name of the task to mark as completed: ");
        string taskName = Console.ReadLine();

        Task taskToMark = taskManager.Tasks.Find(task => task.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
        if (taskToMark != null)
        {
            taskManager.MarkTaskAsCompleted(taskToMark);
            Console.WriteLine($"Task '{taskName}' marked as completed.");
        }

        else
        {
            Console.WriteLine($"Task '{taskName}' not found.");
        }
    }
}

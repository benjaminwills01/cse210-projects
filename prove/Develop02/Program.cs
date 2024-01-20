using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

public class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(string prompt, string response, string date)
    {
        Entry entry = new Entry(prompt, response, date);
        entries.Add(entry);
    }

    public void DisplayJournal()
    {
        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string fileName)
    {
        entries.Clear();

        try
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string[] entryData = reader.ReadLine().Split(',');
                    if (entryData.Length == 3)
                    {
                        AddEntry(entryData[1], entryData[2], entryData[0]);
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found. No entries loaded.");
        }
    }
}

class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice (1-5): ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    string[] prompts = {
                        "Who was the most interesting person I interacted with today?",
                        "What was the best part of my day?",
                        "How did I see the hand of the Lord in my life today?",
                        "What was the strongest emotion I felt today?",
                        "If I had one thing I could do over today, what would it be?"
                    };
                    string randomPrompt = prompts[new Random().Next(prompts.Length)];
                    
                    Console.WriteLine($"Prompt: {randomPrompt}");
                    
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    journal.AddEntry(randomPrompt, response, date);
                    
                    break;

                    case 2:
                    journal.DisplayJournal();
                    break;

                    case 3:
                    Console.Write("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    break;

                    case 4:
                    Console.Write("Enter the file name to save: ");
                    string saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    break;

                    case 5:
                    Environment.Exit(0);
                    break;

                    default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    break;

            }
        }
    }
}


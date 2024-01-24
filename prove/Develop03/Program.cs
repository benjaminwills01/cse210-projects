using System;
using System.Collections.Generic;
using System.Linq;

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        Hidden = false;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public override string ToString()
    {
        return Hidden ? "______" : Text;
    }
}

class ScriptureReference
{
    public string Reference { get; }

    public ScriptureReference(string reference)
    {
        Reference = reference;
    }

    public override string ToString()
    {
        return Reference;
    }
}

class Scripture
{
    public ScriptureReference Reference { get; }
    private List<Word> Words { get; }

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        Words = text.Split().Select(word => new Word(word)).ToList();
    }

    public void HideRandomWord()
    {
        var visibleWords = Words.Where(word => !word.Hidden).ToList();
        
        if (visibleWords.Any())
        {
            var wordToHide = visibleWords[new Random().Next(visibleWords.Count)];
            wordToHide.Hide();
        }
    }

    public bool IsCompletelyHidden()
    {
        return Words.All(word => word.Hidden);
    }

    public void Display()
    {
        Console.WriteLine($"{Reference} - {string.Join(" ", Words)}");
    }
}

class Program
{
    static void Main()
    {
        string reference = "2 Corinthians 12:9";
        string text = "My grace is all you need, for my power is the greatest when you are weak.";

        var scripture = new Scripture(reference, text);

        while (!scripture.IsCompletelyHidden())
        {
            Console.WriteLine("Press Enter to continue or type 'quit' to exit:");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "quit")
                break;

            scripture.HideRandomWord();
            scripture.Display();
        }

        Console.WriteLine("You've hidden all the words. Good job!");
    }
}

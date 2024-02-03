using System;

namespace MindfulnessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nMenu Options:");
                Console.WriteLine("1. Start breathing activity");
                Console.WriteLine("2. Start reflection activity");
                Console.WriteLine("3. Start listing activity");
                Console.WriteLine("4. Quit");

                Console.Write("Select a choice from the menu: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Activity activity;
                switch (choice)
                {
                    case 1:
                        activity = new BreathingActivity();
                        break;
                    case 2:
                        activity = new ReflectionActivity();
                        break;
                    case 3:
                        activity = new ListingActivity();
                        break;
                    case 4:
                        Console.WriteLine();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        continue;
                }
                
                activity.Start();
            }
        }
    }
}

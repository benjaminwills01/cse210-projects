using System;
using System.Threading;
using System.Collections.Generic;

namespace MindfulnessApp
{
    class ListingActivity : Activity
    {
        public override void Start()
        {
            Console.WriteLine("Welcome to the Listing Activity");
            Console.WriteLine();
            Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
            Console.WriteLine();

             string[] questions = {

                "Who are the people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",

            };

            Random rand = new Random();
            string question = questions[rand.Next(questions.Length)];

            int duration = GetDuration();

            Console.WriteLine("Get ready...");
            Animate();
            Console.WriteLine();
            Thread.Sleep(2000);

            Console.WriteLine($"List as many things as you can to the following prompt:");
            Console.WriteLine();
            Console.WriteLine(question);
            Console.WriteLine();

            Console.WriteLine("You will begin shortly...");
            Thread.Sleep(3000);

            Console.WriteLine("Start listing your items now:");
            Console.WriteLine();

            List<string> itemsList = new List<string>();

            int startTime = Environment.TickCount;
            int remainingTime = duration;

            while (remainingTime > 0)
            {
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    itemsList.Add(item);

                    int currentTime = Environment.TickCount;
                    int elapsedTime = (currentTime - startTime) / 1000;
                    remainingTime = duration - elapsedTime;

                    if (remainingTime <= 0)
                        break;
                }

                else
                {
                    break;
                }
            }

            Console.WriteLine($"You listed {itemsList.Count} items!");
            Console.WriteLine();

            Console.WriteLine("Well done! You have completed another {0} seconds of the Listing Activity.", duration);
            Thread.Sleep(3000);
        }
    }
}

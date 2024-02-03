using System;
using System.Threading;

namespace MindfulnessApp
{
    class ReflectionActivity : Activity
    {
        public override void Start()
        {
            Console.WriteLine("Welcome to the Reflection Activity");
            Console.WriteLine();
            Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
            Console.WriteLine();

            string[] prompts = {

                "Think about a time when you faced a challenge and overcame it.",
                "Recall a moment when you felt proud of yourself.",
                "Consider a difficult situation you encountered.",

            };

            Random rand = new Random();
            string prompt = prompts[rand.Next(prompts.Length)];

            int duration = GetDuration();

            Console.WriteLine("Get ready...");
            Animate();
            Console.WriteLine();
            Thread.Sleep(2000); 

            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine();

            Console.WriteLine("When you have something in mind, press enter to continue.");
            Console.WriteLine();

            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

            Console.WriteLine("Now ponder on each of the following questions as they related to this experience.");
            Console.WriteLine();
            
            string[] questions = {

                "What emotions did you experience during that time?",
                "How did the experience change your perspective on life?",
                "What lessons did you learn from overcoming that challenge?",

            };

            int questionDuration = duration / questions.Length;

            foreach (string question in questions)

            {
                Console.WriteLine(question);
                Thread.Sleep(questionDuration * 1000);
            }

            Console.WriteLine("Well done!!");
            Console.WriteLine();
            Console.WriteLine("You have completed another {0} seconds of the Reflection Activity.", duration);
            Thread.Sleep(3000);
        }
    }
}

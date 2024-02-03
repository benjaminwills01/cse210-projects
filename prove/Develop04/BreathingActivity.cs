using System;
using System.Threading;

namespace MindfulnessApp
{
    class BreathingActivity : Activity
    {
        public override void Start()
        {
            Console.WriteLine("Welcome to the Breathing Activity");
            Console.WriteLine();
            Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
            Console.WriteLine();

            int duration = GetDuration();

            Console.WriteLine("Get ready...");
            Animate();
            Console.WriteLine();
            Thread.Sleep(2000);

            int remainingTime = duration;
            while (remainingTime > 0)
            {
                Console.WriteLine("Breathe in...");
                Thread.Sleep(3000);
                remainingTime -= 3;
                if (remainingTime <= 0) break;

                Console.WriteLine("Breathe out...");
                Thread.Sleep(3000);
                remainingTime -= 3;
            }

            Console.WriteLine("Well done!!");
            Console.WriteLine();
            Console.WriteLine("You have completed another {0} seconds of the Breathing Activity.", duration);
            Thread.Sleep(3000);
        }
    }
}

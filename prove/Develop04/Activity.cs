using System;

namespace MindfulnessApp
{
    abstract class Activity
    {
        public abstract void Start();

        protected int GetDuration()
        {
            int duration;

            do
            {
                Console.Write("How long, in seconds, would you like for your session? ");
            } while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0);

            return duration;
        }

        protected void Animate()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write("-");
                Thread.Sleep(500);
                Console.Write("\b \b");
                Console.Write("+"); 
                Thread.Sleep(500);
                Console.Write("\b \b");
            }
        }
    }
}

using System;

namespace Breakout_Clone
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Breakout game = new Breakout())
            {
                game.Run();
            }
        }
    }
}


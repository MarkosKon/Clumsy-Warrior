using System;

namespace Clumsy_Knight
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MainFunction game = new MainFunction())
            {
                game.Run();
            }
        }
    }
#endif
}


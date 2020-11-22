using System;
using Geisha.Engine.Windows;

namespace FlappyBird
{
    // TODO Update screen shot (new window title).
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            GeishaEngineForWindows.Run(new FlappyBirdGame());
        }
    }
}
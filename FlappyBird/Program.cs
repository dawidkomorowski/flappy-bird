using System;
using Geisha.Engine.Windows;

namespace FlappyBird
{
    // TODO Use Animation system of the engine instead of custom code in BirdFlapAnimationComponent.
    // TODO Fix warnings about possible null reference exceptions.
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
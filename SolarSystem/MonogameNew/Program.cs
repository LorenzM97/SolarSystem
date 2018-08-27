using System;

namespace Monogame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public class Program
    {

        [STAThread]
        public static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }

        public string returnPath()
        {
            string folder = Environment.CurrentDirectory;
            return folder;
        }
    }
#endif
}

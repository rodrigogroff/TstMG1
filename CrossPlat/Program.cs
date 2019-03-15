using System;

namespace GameSystem
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameLooper())
            {
                game.Run();
            }                
        }
    }
}

using System;

namespace Gujitsu
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameLooper())
                game.Run();
        }
    }
}

using System.Diagnostics;
using Onlooker.Monogame;

namespace Onlooker;

public static class Program
{
    public const string Name = "Onlooker";
    
    [STAThread]
    public static void Main()
    {
        using var game = new GameManager(Name);
        
        game.Run();
        
        #if DEBUG

        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

        #endif
    }
}
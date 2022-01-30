using Onlooker.Monogame;

namespace Onlooker;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        using var game = new GameManager("Onlooker");
        
        game.Run();
    }
}
using System.Diagnostics;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;

namespace Onlooker;

public static class Program
{
    public const string Name = "Onlooker";
    
    [STAThread]
    public static void Main()
    {
        #if DEBUG
        Console.WriteLine("Open config logs?");

        switch (Console.ReadLine()?.ToLower())
        {
            case "yes":
            case "ye":
            case "y":
            case "ok":
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                ExecuteCommandLine($@"explorer ""{appData}\{Name}\logs\{AppLogger.ConfigLog}""");
                break;
        }
        #endif
        
        using var game = new GameManager(Name);
        
        game.Run();
    }
    
    private static void ExecuteCommandLine(string command)
    {

        var processInfo = new ProcessStartInfo("cmd.exe", "/K " + command)
        {
            CreateNoWindow = true,
            UseShellExecute = false
        };

        Process.Start(processInfo);
    }
}
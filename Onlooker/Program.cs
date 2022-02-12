using System.Diagnostics;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;

namespace Onlooker;

public static class Program
{
    public const string Name = "Onlooker";
    public static string LogDirectory = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        Name, "logs");
    
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
                ExecuteCommandLine($"explorer \"{LogDirectory}\"");
                break;
        }
        #endif

        using var game = new GameManager(Name, LogDirectory);
        
        game.Run();
        
        #if DEBUG

        while (Console.ReadKey().Key != ConsoleKey.Enter) { }

        #endif
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
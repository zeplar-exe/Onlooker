using System.Collections.Concurrent;
using System.Text;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.MethodOutput.OutputTypes;

namespace Onlooker.Monogame.Logging;

public static class AppLogger
{
    private static ConcurrentDictionary<string, StreamWriter> StreamCache { get; }

    public const string LoadingLog = "configuration_loading.log";
    public const string ErrorLog = "error.log";

    public static string? LogDirectory { get; set; }

    static AppLogger()
    {
        StreamCache = new ConcurrentDictionary<string, StreamWriter>();
    }

    public static void Log(string fileName, LogMessageBuilder builder)
    {
        Log(fileName, builder.ToString());
    }

    public static async void Log(string fileName, string log)
    {
        if (!StreamCache.TryGetValue(fileName, out var writer))
        {
            var location = Path.Join(LogDirectory, fileName);

            if (File.Exists(location))
            {
                writer = new StreamWriter(location, true);
            }
            else
            {
                writer = new StreamWriter(
                    File.Open(location, FileMode.Append, FileAccess.Write, FileShare.Read),
                    Encoding.Default);
            }
            
            writer.AutoFlush = false;
            StreamCache[fileName] = writer;
        }
        
        await writer.WriteLineAsync(log);
    }

    public static async Task FlushAsync()
    {
        foreach (var stream in StreamCache)
        {
            await stream.Value.FlushAsync();
        }
    }

    public static async void Dispose()
    {
        foreach (var writer in StreamCache.Values)
        {
            await writer.FlushAsync();
            await writer.DisposeAsync();
        }
    }
}
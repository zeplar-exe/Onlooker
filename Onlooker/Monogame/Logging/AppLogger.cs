using System.Collections.Concurrent;
using System.Text;

namespace Onlooker.Monogame.Logging;

public class AppLogger : IAsyncDisposable
{
    private ConcurrentDictionary<string, StreamWriter> StreamCache { get; }

    public const string LoadingLog = "configuration_loading.log";
    public const string ErrorLog = "error.log";

    public string LogDirectory { get; }

    public static AppLogger Current => GameManager.Current.Logger;
    
    public AppLogger(string logDirectory)
    {
        LogDirectory = logDirectory;
        StreamCache = new ConcurrentDictionary<string, StreamWriter>();
    }

    public void Log(string fileName, LogMessageBuilder builder)
    {
        Log(fileName, builder.ToString());
    }

    public async void Log(string fileName, string log)
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

    public async Task FlushAsync()
    {
        foreach (var stream in StreamCache)
        {
            await stream.Value.FlushAsync();
        }
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var writer in StreamCache.Values)
        {
            await writer.FlushAsync();
            await writer.DisposeAsync();
        }
        
        GC.SuppressFinalize(this);
    }
}
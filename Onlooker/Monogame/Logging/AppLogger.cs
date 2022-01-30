using System.Collections.Concurrent;
using System.Text;

namespace Onlooker.Monogame.Logging;

public class AppLogger : IAsyncDisposable
{
    private ConcurrentDictionary<string, StreamWriter> StreamCache { get; }

    public const string ConfigLog = "configuration_loading.log";
    
    public string LogDirectory { get; }
    
    public AppLogger(string logDirectory)
    {
        LogDirectory = logDirectory;
        StreamCache = new ConcurrentDictionary<string, StreamWriter>();
    }

    public void Log(string file, LogMessageBuilder builder)
    {
        Log(file, builder.ToString());
    }

    public async void Log(string file, string log)
    {
        if (!StreamCache.TryGetValue(file, out var writer))
        {
            var location = Path.Join(LogDirectory, file);

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
            StreamCache[file] = writer;
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
        foreach (var stream in StreamCache)
        {
            await stream.Value.FlushAsync();
            await stream.Value.DisposeAsync();
        }
        
        GC.SuppressFinalize(this);
    }
}
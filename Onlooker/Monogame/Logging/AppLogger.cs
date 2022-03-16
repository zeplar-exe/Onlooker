using System.Collections.Concurrent;
using System.Text;

namespace Onlooker.Monogame.Logging;

public class AppLogger
{
    private ConcurrentDictionary<string, StreamWriter> StreamCache { get; }
    private static ConcurrentDictionary<string, AppLogger> Channels { get; }

    public string LogDirectory { get; }

    static AppLogger()
    {
        Channels = new ConcurrentDictionary<string, AppLogger>();
    }

    private AppLogger(string logDirectory)
    {
        StreamCache = new ConcurrentDictionary<string, StreamWriter>();
        LogDirectory = logDirectory;
    }
    
    public static AppLogger Channel(string directory)
    {
        return Channels.GetOrAdd(directory, d => new AppLogger(d));
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

    public static async Task FlushAll()
    {
        foreach (var channel in Channels)
        {
            await channel.Value.FlushAsync();
        }
    }

    public async void Dispose()
    {
        foreach (var (key, _) in StreamCache)
        {
            if (!StreamCache.Remove(key, out var writer))
                continue;
            
            await writer.FlushAsync();
            await writer.DisposeAsync();
        }
    }
    
    public static void DisposeAll()
    {
        foreach (var channel in Channels)
        {
            channel.Value.Dispose();
        }
    }
}
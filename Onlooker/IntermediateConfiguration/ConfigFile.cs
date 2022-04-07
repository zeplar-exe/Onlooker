using Onlooker.Common.StringResources.Configuration;
using SettingsConfig;
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration;

public abstract class ConfigFile : IDisposable
{
    private const uint SerializationDepth = 100u;

    public const string Extension = ".yasf";

    [SettingsDeserializer.Ignore]
    public FileInfo Source { get; }
    
    protected ConfigFile(FileInfo source)
    {
        Source = source;
    }

    public static IEnumerable<TConfig> FromDirectory<TConfig>(DirectoryInfo directory) where TConfig : ConfigFile
    {
        return directory
            .EnumerateFiles($"*{Extension}", SearchOption.TopDirectoryOnly)
            .Select(file => (TConfig)Activator.CreateInstance(typeof(TConfig), file)!);
    }

    public virtual IEnumerable<ConfigUpdateStatus> UpdateFromStream(Stream stream)
    {
        SettingsDeserializer.DeserializeTo(SettingsDocument.FromStream(stream), this);

        yield return new ConfigUpdateStatus(
            string.Format(ConfigurationProgress.FileLoaded, Source),
            UpdateStatusType.Success);
    }

    public virtual IEnumerable<ConfigWriteStatus> WriteToStream(Stream stream)
    {
        var serializer = new SettingsSerializer(SerializationDepth);
        var document = serializer.Serialize(this);
        
        Exception? exception = null;

        try
        {
            using var writer = new StreamWriter(stream);
            writer.Write(document.ToString());
        }
        catch (Exception e)
        {
            exception = e;
        }
        
        if (exception == null)
        {
            yield return new ConfigWriteStatus(
                string.Format(ConfigurationProgress.DeserializeFailure, Source.Name), 
                WriteStatusType.Success);
        }
        else
        {
            yield return new ConfigWriteStatus(
                string.Format(ConfigurationProgress.DeserializeSuccess, Source.Name, exception.GetType().Name),
                WriteStatusType.Corruption);
        }
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
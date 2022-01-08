using SettingsConfig;
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration;

public abstract class ConfigFile
{
    private const uint SerializationDepth = 100u;

    public const string Extension = ".txt";

    [SettingsDeserializer.Ignore]
    public FileInfo Source { get; }
    
    protected ConfigFile(FileInfo source)
    {
        Source = source;
    }
    
    public virtual ConfigUpdateStatus UpdateFromStream(Stream stream)
    {
        SettingsDeserializer.DeserializeTo(SettingsDocument.FromStream(stream), this);

        return new ConfigUpdateStatus("Success", UpdateStatusType.Success);
    }

    public virtual ConfigWriteStatus WriteToStream(Stream stream)
    {
        var serializer = new SettingsSerializer(SerializationDepth);
        var document = serializer.Serialize(this);

        try
        {
            using var writer = new StreamWriter(stream);
            writer.Write(document.ToString());
        }
        catch (Exception e)
        {
            return new ConfigWriteStatus(e.ToString(), WriteStatusType.Corruption);
        }

        return new ConfigWriteStatus("Success", WriteStatusType.Success);
    }
}
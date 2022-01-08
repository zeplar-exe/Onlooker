using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Entities.Moods;

public class MoodConfig : DescriptiveConfigFile
{
    [SettingsSerializer.SerializationName("stat_offset")]
    [SettingsDeserializer.SerializationName("stat_offset")]
    public List<int> StatOffset { get; set; }

    public MoodConfig(FileInfo source) : base(source)
    {
        
    }
}
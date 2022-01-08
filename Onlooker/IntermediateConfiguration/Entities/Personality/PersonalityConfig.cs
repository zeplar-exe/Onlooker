using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Entities.Personality;

public class PersonalityConfig : DescriptiveConfigFile
{
    [SettingsSerializer.SerializationName("stat_offset")]
    [SettingsDeserializer.SerializationName("stat_offset")]
    public List<int> StatOffset { get; set; }

    public PersonalityConfig(FileInfo source) : base(source)
    {
        
    }
}
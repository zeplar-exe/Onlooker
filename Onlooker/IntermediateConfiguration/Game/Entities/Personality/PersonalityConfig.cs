using SettingsConfig.Serialization;
using SettingsConfig.Settings;

namespace Onlooker.IntermediateConfiguration.Game.Entities.Personality;

public class PersonalityConfig : DescriptiveConfigFile
{
    [SettingsSerializer.SerializationName("stat_offset")]
    [SettingsDeserializer.SerializationName("stat_offset")]
    public Dictionary<string, SettingValue> StatOffset { get; set; }

    public PersonalityConfig(FileInfo source) : base(source)
    {
        
    }
}
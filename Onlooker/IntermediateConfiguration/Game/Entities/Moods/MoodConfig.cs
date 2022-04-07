using YASF.Serialization;
using YASF.Settings;

namespace Onlooker.IntermediateConfiguration.Game.Entities.Moods;

public class MoodConfig : DescriptiveConfigFile
{
    [SettingsSerializer.SerializationName("stat_offset")]
    [SettingsDeserializer.SerializationName("stat_offset")]
    public Dictionary<string, SettingValue> StatOffset { get; set; }

    public MoodConfig(FileInfo source) : base(source)
    {
        StatOffset = new Dictionary<string, SettingValue>();
    }
}
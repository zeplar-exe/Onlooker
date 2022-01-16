using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Game.Entities.Races;

public class RaceConfig : DescriptiveConfigFile
{
    [SettingsDeserializer.SerializationName("reproduction_method")]
    public ReproductionMethod ReproductionMethod { get; set; }
    [SettingsDeserializer.SerializationName("maximum_knowledge")]
    public ulong MaximumKnowledge { get; set; }
    [SettingsDeserializer.SerializationName("move_speed")]
    public ulong MoveSpeed { get; set; }

    public RaceConfig(FileInfo source) : base(source)
    {
        
    }
}
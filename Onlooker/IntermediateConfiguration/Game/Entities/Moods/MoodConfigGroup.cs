namespace Onlooker.IntermediateConfiguration.Game.Entities.Moods;

public class MoodConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/game/entities/moods")]
    public List<MoodConfig> MoodConfigs { get; }
    
    public MoodConfigGroup()
    {
        MoodConfigs = new List<MoodConfig>();
    }
}
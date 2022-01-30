namespace Onlooker.IntermediateConfiguration.Game.Entities.Moods;

public class MoodConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("")] 
    public List<MoodConfig> MoodConfigs { get; }
    
    public MoodConfigGroup()
    {
        MoodConfigs = new List<MoodConfig>();
    }
}
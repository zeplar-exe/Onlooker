namespace Onlooker.IntermediateConfiguration.Game.Entities.Moods;

public class MoodConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("moods")] 
    public List<MoodConfig> MoodConfigs { get; }
    
    public MoodConfigGroup()
    {
        MoodConfigs = new List<MoodConfig>();
    }
}
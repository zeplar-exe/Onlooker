namespace Onlooker.IntermediateConfiguration.Game.Entities.Races;

public class RaceConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("game/entities/races")]
    public List<RaceConfig> RaceConfigs { get; }

    public RaceConfigGroup()
    {
        RaceConfigs = new List<RaceConfig>();
    }
}
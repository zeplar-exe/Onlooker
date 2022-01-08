using Onlooker.IntermediateConfiguration.Entities.Moods;
using Onlooker.IntermediateConfiguration.Entities.Personality;
using Onlooker.IntermediateConfiguration.Entities.Races;
using Onlooker.IntermediateConfiguration.Entities.Stats;

namespace Onlooker.IntermediateConfiguration.Entities;

public class EntitiesConfigGroup : ConfigGroup
{
    public RaceConfigGroup RaceConfig { get; }
    public StatsConfigGroup StatsConfig { get; }
    public PersonalityConfigGroup PersonalityConfig { get; }
    public MoodConfigGroup MoodConfig { get; }
    
    public EntitiesConfigGroup()
    {
        RaceConfig = new RaceConfigGroup();
        StatsConfig = new StatsConfigGroup();
        PersonalityConfig = new PersonalityConfigGroup();
        MoodConfig = new MoodConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        RaceConfig.UpdateFromDirectory(root.CreateSubdirectory("races"), progress);
        StatsConfig.UpdateFromDirectory(root.CreateSubdirectory("stats"), progress);
        PersonalityConfig.UpdateFromDirectory(root.CreateSubdirectory("personalities"), progress);
        MoodConfig.UpdateFromDirectory(root.CreateSubdirectory("moods"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        throw new NotImplementedException();
    }
}
using Onlooker.IntermediateConfiguration.Game.Entities.Moods;
using Onlooker.IntermediateConfiguration.Game.Entities.Personality;
using Onlooker.IntermediateConfiguration.Game.Entities.Races;
using Onlooker.IntermediateConfiguration.Game.Entities.Stats;

namespace Onlooker.IntermediateConfiguration.Game.Entities;

public class EntitiesConfigGroup : ConfigGroup
{
    public RaceConfigGroup Races { get; }
    public StatsConfigGroup Stats { get; }
    public PersonalityConfigGroup Personalities { get; }
    public MoodConfigGroup Moods { get; }
    
    public EntitiesConfigGroup()
    {
        Races = new RaceConfigGroup();
        Stats = new StatsConfigGroup();
        Personalities = new PersonalityConfigGroup();
        Moods = new MoodConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        Races.UpdateFromDirectory(root.CreateSubdirectory("races"), progress);
        Stats.UpdateFromDirectory(root.CreateSubdirectory("stats"), progress);
        Personalities.UpdateFromDirectory(root.CreateSubdirectory("personalities"), progress);
        Moods.UpdateFromDirectory(root.CreateSubdirectory("moods"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        Races.WriteToDirectory(root.CreateSubdirectory("races"), progress);
        Stats.WriteToDirectory(root.CreateSubdirectory("stats"), progress);
        Personalities.WriteToDirectory(root.CreateSubdirectory("personalities"), progress);
        Moods.WriteToDirectory(root.CreateSubdirectory("moods"), progress);
    }
}
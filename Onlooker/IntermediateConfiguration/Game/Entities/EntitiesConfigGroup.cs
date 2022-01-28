using Onlooker.IntermediateConfiguration.Game.Entities.Moods;
using Onlooker.IntermediateConfiguration.Game.Entities.Personality;
using Onlooker.IntermediateConfiguration.Game.Entities.Races;
using Onlooker.IntermediateConfiguration.Game.Entities.Stats;

namespace Onlooker.IntermediateConfiguration.Game.Entities;

public class EntitiesConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("races")] public RaceConfigGroup Races { get; set; }
    [RelativeConfigLocation("stats")] public StatsConfigGroup Stats { get; set; }
    [RelativeConfigLocation("personalities")] public PersonalityConfigGroup Personalities { get; set; }
    [RelativeConfigLocation("moods")] public MoodConfigGroup Moods { get; set; }
}
using Onlooker.IntermediateConfiguration.Game.Entities.Moods;
using Onlooker.IntermediateConfiguration.Game.Entities.Personality;
using Onlooker.IntermediateConfiguration.Game.Entities.Races;
using Onlooker.IntermediateConfiguration.Game.Entities.Stats;

namespace Onlooker.IntermediateConfiguration.Game.Entities;

public class EntitiesConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/game/entities/races")] public RaceConfigGroup Races { get; set; }
    [ConfigLocation("configuration/game/entities/stats")] public StatsConfigGroup Stats { get; set; }
    [ConfigLocation("configuration/game/entities/personalities")] public PersonalityConfigGroup Personalities { get; set; }
    [ConfigLocation("configuration/game/entities/moods")] public MoodConfigGroup Moods { get; set; }
}
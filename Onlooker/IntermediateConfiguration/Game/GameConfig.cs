using Onlooker.IntermediateConfiguration.Game.Entities;
using Onlooker.IntermediateConfiguration.Game.World;

namespace Onlooker.IntermediateConfiguration.Game;

public class GameConfig : ConfigGroup
{
    [RelativeConfigLocation("entities")] public EntitiesConfigGroup EntitiesConfig { get; set; } 
    [RelativeConfigLocation("world")] public WorldConfigGroup WorldConfig { get; set; }
}
using Onlooker.IntermediateConfiguration.Game.Entities;

namespace Onlooker.IntermediateConfiguration.Game;

public class GameConfig : ConfigGroup
{
    [RelativeConfigLocation("entities")] public EntitiesConfigGroup EntitiesConfig { get; } 

    public GameConfig()
    {
        EntitiesConfig = new EntitiesConfigGroup();
    }
}
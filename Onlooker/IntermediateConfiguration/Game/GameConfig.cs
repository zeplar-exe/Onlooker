using Onlooker.IntermediateConfiguration.Game.Entities;

namespace Onlooker.IntermediateConfiguration.Game;

public class GameConfig : ConfigGroup
{
    [ConfigLocation("configuration/game/entities")] public EntitiesConfigGroup EntitiesConfig { get; } 

    public GameConfig()
    {
        EntitiesConfig = new EntitiesConfigGroup();
    }
}
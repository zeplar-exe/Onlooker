using Onlooker.IntermediateConfiguration.Game.World.Terrain;

namespace Onlooker.IntermediateConfiguration.Game.World;

public class WorldConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/game/world/terrain")]
    public TerrainTypeConfigGroup TerrainTypes { get; set; }
}
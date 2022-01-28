using Onlooker.IntermediateConfiguration.Game.World.Terrain;

namespace Onlooker.IntermediateConfiguration.Game.World;

public class WorldConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("terrain")]
    public TerrainTypeConfigGroup TerrainTypes { get; set; }
}
using Onlooker.Common.Extensions;

namespace Onlooker.IntermediateConfiguration.Game.World.Terrain;

public class TerrainTypeConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("")]
    public List<TerrainTypeConfig> TypeConfigs { get; }

    public TerrainTypeConfigGroup()
    {
        TypeConfigs = new List<TerrainTypeConfig>();
    }
}
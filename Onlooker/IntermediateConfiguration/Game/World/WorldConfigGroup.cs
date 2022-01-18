using Onlooker.IntermediateConfiguration.Game.World.Terrain;

namespace Onlooker.IntermediateConfiguration.Game.World;

public class WorldConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/game/world/terrain")]
    public TerrainTypeConfigGroup TerrainTypes { get; }

    public WorldConfigGroup()
    {
        TerrainTypes = new TerrainTypeConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        TerrainTypes.UpdateFromDirectory(root.CreateSubdirectory("terrain_types"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        TerrainTypes.WriteToDirectory(root.CreateSubdirectory("terrain_types"), progress);
    }
}
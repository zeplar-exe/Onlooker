namespace Onlooker.IntermediateConfiguration.World;

public class WorldConfigGroup : ConfigGroup
{
    public TerrainTypeConfigGroup TerrainTypeConfigGroup { get; }

    public WorldConfigGroup()
    {
        TerrainTypeConfigGroup = new TerrainTypeConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        TerrainTypeConfigGroup.UpdateFromDirectory(root.CreateSubdirectory("terrain_types"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        TerrainTypeConfigGroup.WriteToDirectory(root.CreateSubdirectory("terrain_types"), progress);
    }
}
using Onlooker.IntermediateConfiguration.Entities;

namespace Onlooker.IntermediateConfiguration;

public class GameConfig : ConfigGroup
{
    public EntitiesConfigGroup EntitiesConfig { get; }

    public GameConfig()
    {
        EntitiesConfig = new EntitiesConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        EntitiesConfig.UpdateFromDirectory(root.CreateSubdirectory("entities"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        EntitiesConfig.WriteToDirectory(root.CreateSubdirectory("entities"), progress);
    }
}
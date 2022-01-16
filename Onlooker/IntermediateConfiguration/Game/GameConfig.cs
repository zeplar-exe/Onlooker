using Onlooker.IntermediateConfiguration.Game.Entities;

namespace Onlooker.IntermediateConfiguration.Game;

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
        
        progress.Report(new ConfigUpdateStatus("Loading Completed", UpdateStatusType.Completed));
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        EntitiesConfig.WriteToDirectory(root.CreateSubdirectory("entities"), progress);
    }
}
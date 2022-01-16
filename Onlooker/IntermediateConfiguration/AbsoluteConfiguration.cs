using Onlooker.IntermediateConfiguration.Common;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration;

public class AbsoluteConfiguration : ConfigGroup
{
    public CommonConfig CommonConfig { get; }
    public GameConfig GameConfig { get; }

    public AbsoluteConfiguration()
    {
        CommonConfig = new CommonConfig();
        GameConfig = new GameConfig();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        CommonConfig.UpdateFromDirectory(root.CreateSubdirectory("common"), progress);
        GameConfig.UpdateFromDirectory(root.CreateSubdirectory("game"), progress);
        
        progress.Report(new ConfigUpdateStatus("Loading Completed", UpdateStatusType.Completed));
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        CommonConfig.WriteToDirectory(root.CreateSubdirectory("common"), progress);
        GameConfig.WriteToDirectory(root.CreateSubdirectory("game"), progress);
        
        progress.Report(new ConfigWriteStatus("Writing Completed", WriteStatusType.Completed));
    }
}
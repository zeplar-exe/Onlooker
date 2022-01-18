using Onlooker.IntermediateConfiguration.Common;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration;

public class AbsoluteConfiguration : ConfigGroup
{
    [ConfigLocation("configuration/common")] public CommonConfig CommonConfig { get; }
    [ConfigLocation("configuration/game")] public GameConfig GameConfig { get; }

    public AbsoluteConfiguration()
    {
        CommonConfig = new CommonConfig();
        GameConfig = new GameConfig();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        base.UpdateFromDirectory(root, progress);
        
        progress.Report(new ConfigUpdateStatus("Loading Completed", UpdateStatusType.Completed));
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        base.WriteToDirectory(root, progress);
        
        progress.Report(new ConfigWriteStatus("Writing Completed", WriteStatusType.Completed));
    }
}
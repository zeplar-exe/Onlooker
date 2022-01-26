using Onlooker.IntermediateConfiguration.Common;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.IntermediateConfiguration.GUI;

namespace Onlooker.IntermediateConfiguration;

public class AbsoluteConfiguration : ConfigGroup
{
    [ConfigLocation("configuration/common")] public CommonConfig CommonConfig { get; set; }
    [ConfigLocation("configuration/game")] public GameConfig GameConfig { get; set; }
    [ConfigLocation("configuration/gui")] public GuiConfigGroup GuiConfig { get; set; }

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
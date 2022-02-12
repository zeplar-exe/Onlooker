using Onlooker.IntermediateConfiguration.Common;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.IntermediateConfiguration.GUI;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration;

public class ConfigurationRoot : ConfigGroup
{
    [RelativeConfigLocation("common")] public CommonConfig CommonConfig { get; set; }
    [RelativeConfigLocation("game")] public GameConfig GameConfig { get; set; }
    [RelativeConfigLocation("gui")] public GuiConfigGroup GuiConfig { get; set; }

    public static ConfigurationRoot Current => GameManager.Current.Configuration;

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
using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Settings;

namespace Onlooker.IntermediateConfiguration.Modules.Settings;

public class SettingsModule : IModule
{
    public GuiSettingsConfig GuiSettings { get; set; }
    public LogSettingsConfig LogSettings { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("settings");

        GuiSettings = new GuiSettingsConfig(directory.ToRelativeFile("gui.yasf"));
        GuiSettings.UpdateAndLogFromStream(GuiSettings.Source.OpenRead());

        LogSettings = new LogSettingsConfig(directory.ToRelativeFile("log.yasf"));
        LogSettings.UpdateAndLogFromStream(LogSettings.Source.OpenRead());
    }

    public void Write(ModuleRoot root)
    {
        GuiSettings.WriteAndLogToStream(GuiSettings.Source.OpenRead());
        LogSettings.WriteAndLogToStream(LogSettings.Source.OpenRead());
    }
}
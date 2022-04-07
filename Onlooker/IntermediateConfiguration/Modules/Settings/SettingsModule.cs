using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Settings;

namespace Onlooker.IntermediateConfiguration.Modules.Settings;

public class SettingsModule : IModule
{
    public GuiSettingsConfig GuiSettings { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("settings");

        GuiSettings = new GuiSettingsConfig(directory.ToRelativeFile("gui.yasf"));
        GuiSettings.UpdateAndLogFromStream(directory.ToRelativeFile("gui.yasf").OpenRead());
    }

    public void Write(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("settings");
        
        GuiSettings.WriteAndLogToStream(directory.ToRelativeFile("gui.yasf").OpenWrite());
    }
}
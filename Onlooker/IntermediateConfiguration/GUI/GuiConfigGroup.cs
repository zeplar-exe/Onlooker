using Onlooker.IntermediateConfiguration.GUI.Processing;

namespace Onlooker.IntermediateConfiguration.GUI;

public class GuiConfigGroup : ConfigGroup
{
    [RelativeConfigLocation("main_menu.interface")] public GuiDocument MainMenu { get; set; }
}
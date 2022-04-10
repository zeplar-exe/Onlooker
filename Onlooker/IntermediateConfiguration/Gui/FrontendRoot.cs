using Onlooker.IntermediateConfiguration.Gui.Elements;

namespace Onlooker.IntermediateConfiguration.Gui;

public class FrontendRoot
{
    public List<GuiElement> ChildElements { get; }

    public FrontendRoot()
    {
        ChildElements = new List<GuiElement>();
    }
}
using Onlooker.IntermediateConfiguration.GUI.Elements;

namespace Onlooker.IntermediateConfiguration.GUI.Processing;

public class GuiDocument
{
    public RootElement Root { get; }

    public GuiDocument()
    {
        Root = new RootElement();
    }
}
using System.Xml.Linq;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class RootElement : GuiElement
{
    public override bool IsLocked()
    {
        return false;
    }

    public override void LoadFromXml(XElement element)
    {
        
    }
}
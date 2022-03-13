using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties;

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
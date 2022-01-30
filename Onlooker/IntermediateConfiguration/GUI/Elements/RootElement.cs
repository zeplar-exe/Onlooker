using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Monogame;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class RootElement : GuiElement
{
    public RootElement() : base(new RectangleProperty(Rectangle.Empty))
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }

    public override void LoadFromXml(XElement element)
    {
        
    }
}
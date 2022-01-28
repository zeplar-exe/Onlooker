using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Monogame;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class RootElement : GuiElement
{
    public RootElement() : base(new RectangleProperty(Rectangle.Empty))
    {
        
    }
    
    public override void Update(GameTime time)
    {
        foreach (var child in Children)
            child.Update(time);
    }

    public override bool IsLocked()
    {
        return false;
    }

    public override void LoadFromXml(XElement element)
    {
        throw new NotImplementedException();
    }
}
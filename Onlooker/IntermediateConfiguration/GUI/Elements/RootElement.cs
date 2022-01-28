using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public class RootElement : GuiElement
{
    public override void Update(GameTime time)
    {
        foreach (var child in Children)
            child.Update(time);
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        foreach (var child in Children)
            child.Draw(canvas, time);
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
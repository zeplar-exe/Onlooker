using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Monogame;
using Onlooker.Monogame.Controllers;
using Onlooker.ObjectProperties;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public abstract class GuiElement : GameController
{
    public RectangleProperty Rect { get; }
    public List<GuiElement> Children { get; }
    public int ZIndex { get; }

    public GuiElement(RectangleProperty rect)
    {
        Rect = rect;
        Children = new List<GuiElement>();
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        foreach (var child in Children)
            child.Draw(canvas, time);
    }

    public abstract void LoadFromXml(XElement element);
}
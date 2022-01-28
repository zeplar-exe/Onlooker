using System.Xml.Linq;
using Onlooker.Monogame.Controllers;

namespace Onlooker.IntermediateConfiguration.GUI.Elements;

public abstract class GuiElement : GameController
{
    public List<GuiElement> Children { get; }
    public int ZIndex { get; }

    public abstract void LoadFromXml(XElement element);
}
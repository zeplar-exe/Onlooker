using Microsoft.Xna.Framework;
using Onlooker.IntermediateConfiguration.Gui.Elements;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.IntermediateConfiguration.Gui;

public class FrontendRoot
{
    public List<GuiElement> ChildElements { get; }

    public FrontendRoot()
    {
        ChildElements = new List<GuiElement>();
    }
}
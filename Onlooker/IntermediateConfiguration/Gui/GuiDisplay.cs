using Microsoft.Xna.Framework;
using Onlooker.IntermediateConfiguration.Gui.Elements;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.IntermediateConfiguration.Gui;

public class GuiDisplay : GameController
{
    public EmptyElement Root { get; }
    public Rectangle DisplayRect { get; set; }

    public GuiDisplay(Rectangle rectangle)
    {
        Root = new EmptyElement();
        DisplayRect = rectangle;
    }
    
    public override void Update(GameTime time)
    {
        Update(Root, time);
    }

    private void Update(GuiElement element, GameTime time)
    {
        foreach (var child in element.Children)
        {
            
        }
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        Root.Draw(canvas, time);
    }

    public override bool IsLocked()
    {
        return false;
    }
}
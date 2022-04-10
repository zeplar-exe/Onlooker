using Microsoft.Xna.Framework;
using Onlooker.IntermediateConfiguration.Gui.Elements;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.IntermediateConfiguration.Gui;

public class GuiDisplay : GameController
{
    public EmptyElement Root { get; }

    public GuiDisplay()
    {
        Root = new EmptyElement();
    }
    
    public override void Update(GameTime time)
    {
        Root.Update(time);
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
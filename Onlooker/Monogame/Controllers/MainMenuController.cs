using Microsoft.Xna.Framework;

namespace Onlooker.Monogame.Controllers;

public class MainMenuController : GameController
{
    public bool IsShown { get; set; }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }
}
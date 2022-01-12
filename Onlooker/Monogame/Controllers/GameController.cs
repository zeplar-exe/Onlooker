using Microsoft.Xna.Framework;

namespace Onlooker.Monogame.Controllers;

public abstract class GameController
{
    protected GameManager Manager { get; }

    protected GameController(GameManager manager)
    {
        Manager = manager;
    }
    
    public abstract void Update(GameTime time);
    public abstract void Draw(DrawCanvas canvas, GameTime time);
}
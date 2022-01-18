using Microsoft.Xna.Framework;

namespace Onlooker.Monogame.Controllers;

public abstract class GameController
{
    public bool Disposed { get; set; }
    public bool Enabled { get; set; }
    
    public virtual void OnContentLoad() { }
    public abstract void Update(GameTime time);
    public abstract void Draw(DrawCanvas canvas, GameTime time);
    public abstract bool IsLocked();
}
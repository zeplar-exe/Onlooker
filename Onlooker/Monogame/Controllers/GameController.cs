using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.Monogame.Controllers.Transitions;

namespace Onlooker.Monogame.Controllers;

public abstract class GameController
{
    public Guid Id { get; }
    public ControllerCompletionHandler CompletionHandler { get; set; }

    public GameController()
    {
        Id = Guid.NewGuid();
    }

    public bool Disposed { get; set; }
    public bool Enabled { get; set; }
    
    public virtual void OnStart() { }
    public virtual void OnContentLoad() { }
    public abstract void Update(GameTime time);
    public abstract void Draw(DrawCanvas canvas, GameTime time);
    public abstract bool IsLocked();

    public virtual void OnDisposing(CancellationEventArgs args) { }
}
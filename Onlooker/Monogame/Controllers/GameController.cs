using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.Monogame.Controllers.Transitions;
using Onlooker.Monogame.Graphics;

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

    private bool b_enabled;

    public bool Enabled
    {
        get => b_enabled;
        set
        {
            if (b_enabled == value)
                return;

            b_enabled = value;

            if (b_enabled)
                OnEnable();
            else
                OnDisable();
        }
    }

    public virtual void OnStart() { }
    public virtual void OnContentLoad() { }
    public abstract void Update(GameTime time);
    public abstract void Draw(DrawCanvas canvas, GameTime time);
    public abstract bool IsLocked();

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    public virtual void OnDisposing(CancellationEventArgs args) { }
}
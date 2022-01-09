using Microsoft.Xna.Framework;

namespace Onlooker.Monogame;

public abstract class GameHook
{
    protected GameManager Manager { get; set; }
    
    public bool IsEnabled { get; set; }

    internal void SetManager(GameManager manager) => Manager = manager;

    public virtual void OnUpdate(GameTime time) { }
    public virtual void OnDraw(GameTime time) { }

    public abstract bool IsLocked();

    public virtual void Enable() => IsEnabled = true;
    public virtual void Disable() => IsEnabled = false;
}
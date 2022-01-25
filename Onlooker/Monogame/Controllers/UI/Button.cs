using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.ObjectProperties;

namespace Onlooker.Monogame.Controllers.UI;

public class Button : GameController
{
    public Label Label { get; }

    public event EventHandler? OnClick;
    
    public Button(int zIndex)
    {
        Label = new Label(zIndex);
    }

    public override void OnStart()
    {
        GameManager.Current.HookController(Label);
        Label.Enabled = true;
        
        base.OnStart();
    }

    public override void Update(GameTime time)
    {
        if (MouseHelper.IsMouseReleasedOverRect(Label.Rect))
        {
            OnClick?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }
}
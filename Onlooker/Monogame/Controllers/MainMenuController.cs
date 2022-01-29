using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.IntermediateConfiguration.GUI.Processing;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class MainMenuController : GameController
{
    private GuiDocument Gui { get; set; }
    
    public bool IsShown { get; set; }
    
    protected override void OnEnable()
    {
        Gui = GameManager.Current.Configuration.GuiConfig.MainMenu;
        Gui.Root.Enabled = true;
        
        GameManager.Current.HookController(Gui.Root);
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override void OnDisposing(CancellationEventArgs args)
    {
        Gui.Root.Disposed = true;
    }

    public override bool IsLocked()
    {
        return false;
    }
}
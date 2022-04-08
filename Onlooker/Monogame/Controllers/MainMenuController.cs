using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.IntermediateConfiguration.GUI.Processing;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Gui;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class MainMenuController : GameController
{
    public GuiDocument Gui { get; set; }
    
    protected override void OnEnable()
    {
        Gui = ModuleRoot.Current.GetModule<GuiModule>().MainMenu;
        Gui.Root.Enabled = true;
        
        GameManager.Current.HookController(Gui.Root);
    }

    protected override void OnDisable()
    {
        Gui.Root.Enabled = false;
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
using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Gui;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Gui;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class MainMenuController : GameController
{
    public GuiDisplay Gui { get; set; }

    public MainMenuController()
    {
        Gui = new GuiDisplay();
    }
    
    protected override void OnEnable()
    {
        var menuRoot = IModule.Get<GuiModule>().MainMenu;
        
        Gui.Root.Children.Overwrite(menuRoot.ChildElements);
        Gui.Enabled = true;
        
        GameManager.Current.HookController(Gui);
    }

    protected override void OnDisable()
    {
        Gui.Enabled = false;
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override void OnDisposing(CancellationEventArgs args)
    {
        Gui.Disposed = true;
    }

    public override bool IsLocked()
    {
        return false;
    }
}
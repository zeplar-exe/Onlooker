using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Entities;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class LoadingScreenController : GameController
{
    public bool LoadingCompleted { get; private set; }

    public LoadingScreenController()
    {
        
    }

    public async void Load()
    {
        ModuleRoot.Current.GetPersistentModule<CommonGraphicsModule>(); // Create persistent modules
        ModuleRoot.Current.GetPersistentModule<CommonFontsModule>();
        
        LoadingCompleted = true;
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        // Null coalesce is required to prevent errors while config groups have not yet loaded
        var texture = ModuleRoot.Current.GetPersistentModule<CommonGraphicsModule>().LoadingScreen;
        
        canvas.Draw(Layers.PriorityUI, new TextureGraphic(texture, CommonValues.ScreenRect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
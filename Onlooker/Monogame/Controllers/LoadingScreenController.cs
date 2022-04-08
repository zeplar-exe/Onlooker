using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Common;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class LoadingScreenController : GameController
{
    private CommonGraphicsModule GraphicsModule { get; set; }
    
    public bool LoadingCompleted { get; private set; }

    public void Load()
    {
        GraphicsModule = ModuleRoot.Current.GetModule<CommonGraphicsModule>(); // Create persistent modules
        ModuleRoot.Current.GetModule<CommonFontsModule>();
        
        LoadingCompleted = true;
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        // Null coalesce is required to prevent errors while config groups have not yet loaded
        var texture = ModuleRoot.Current.GetModule<CommonGraphicsModule>().LoadingScreen;
        
        canvas.Draw(Layers.PriorityUI, new TextureGraphic(texture, CommonValues.ScreenRect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
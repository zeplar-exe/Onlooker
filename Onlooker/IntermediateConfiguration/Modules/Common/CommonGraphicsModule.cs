using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Modules.Common;

public class CommonGraphicsModule : IModule
{
    public Texture2D LoadingScreen { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("common/graphics");
        var loadingScreen = new FileInfo(Path.Join(directory.FullName, "loading_screen.png"));
                
        if (!loadingScreen.Exists)
        {
            GameManager.Current.Exit();
            
            return;
        }
                
        LoadingScreen = Texture2D.FromFile(GameManager.Current.GraphicsDevice, loadingScreen.FullName);
    }

    public void Write(ModuleRoot root)
    {
        
    }
}
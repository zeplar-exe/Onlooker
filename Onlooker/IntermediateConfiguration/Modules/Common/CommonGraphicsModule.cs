using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Modules.Common;

public class CommonGraphicsModule : IModule
{
    public Texture2D LoadingScreen { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("common/graphics");
        
        LoadingScreen = GetTexture(directory.ToRelativeFile("loading_screen.png"));
    }

    public void Write(ModuleRoot root)
    {
        throw new NotImplementedException();
    }

    private Texture2D GetTexture(FileInfo file)
    {
        if (!file.Exists)
        {
            return TextureHelper.MissingTexture;
        }
                
        return Texture2D.FromFile(GameManager.Current.GraphicsDevice, file.FullName);
    }
}
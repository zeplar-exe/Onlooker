using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Monogame;
using SpriteFontPlus;

namespace Onlooker.IntermediateConfiguration.Modules.Common;

public class CommonFontsModule : IModule
{
    public SpriteFont Information { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("common/fonts");

        var information = GetSpriteFont(directory.ToRelativeFile("information.ttf"));

        if (information == null)
        {
            GameManager.Current.Exit();
            
            return;
        }

        Information = information;
    }

    public void Write(ModuleRoot root)
    {
        throw new NotImplementedException();
    }

    private SpriteFont? GetSpriteFont(FileInfo file)
    {
        if (!file.Exists)
            return null;
                
        var bake = TtfFontBaker.Bake(
            File.ReadAllBytes(file.FullName),
            25,
            1024,
            1024,
            new[]
            {
                CharacterRange.BasicLatin,
                CharacterRange.Latin1Supplement,
                CharacterRange.LatinExtendedA,
                CharacterRange.Cyrillic
            }
        );

        return bake.CreateSpriteFont(GameManager.Current.GraphicsDevice);
    }
}
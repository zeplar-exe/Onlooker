using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Monogame;
using SpriteFontPlus;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class CommonFontsModule : IModule
{
    public SpriteFont Information { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("common/fonts");

        var file = new FileInfo(Path.Join(directory.FullName, "information.ttf"));
                
        if (!file.Exists)
        {
            GameManager.Current.Exit();
            
            return;
        }
                
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

        Information = bake.CreateSpriteFont(GameManager.Current.GraphicsDevice);
    }

    public void Write(ModuleRoot root)
    {
        
    }
}
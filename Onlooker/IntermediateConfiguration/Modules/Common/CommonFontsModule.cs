using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Common.MethodOutput;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;
using SpriteFontPlus;

namespace Onlooker.IntermediateConfiguration.Modules.Common;

public class CommonFontsModule : IModule
{
    public SpriteFont Information { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("common/fonts");
        var (defaultFontOutput, defaultFontValue) = FontHelper.GetDefaultFont("error.ttf");

        if (defaultFontOutput.IsFailure())
        {
            AppLoggerCommon.ErrorLog(defaultFontOutput.CreateLog());
            GameManager.Current.Exit();
            
            return;
        }

        var defaultFont = defaultFontValue!;

        Information = GetSpriteFont(directory.ToRelativeFile("information.ttf")) ?? defaultFont;
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
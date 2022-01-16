using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.Monogame;
using SpriteFontPlus;

namespace Onlooker.IntermediateConfiguration.Common.Fonts;

public class FontsConfigGroup : ConfigGroup
{
    public SpriteFont? Information { get; set; }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        var bake = TtfFontBaker.Bake(File.ReadAllBytes(
                Path.Join(Directory.GetCurrentDirectory(), "configuration/common/fonts/information.ttf")),
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
        
        progress.Report(new ConfigUpdateStatus("Loaded 'common/fonts/information.ttf'", UpdateStatusType.Success));
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        
    }
}
using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration.Common.Fonts;

public class FontsConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/common/fonts/information.ttf")]
    public SpriteFont? Information { get; set; }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        
    }
}
using Onlooker.IntermediateConfiguration.Common.Fonts;
using Onlooker.IntermediateConfiguration.Common.Graphics;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration.Common;

public class CommonConfig : ConfigGroup
{
    [ConfigLocation("configuration/common/graphics")] public GraphicsConfigGroup Graphics { get; set; }
    [ConfigLocation("configuration/common/fonts")] public FontsConfigGroup Fonts { get; set; }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        base.UpdateFromDirectory(root, progress);
    }
}
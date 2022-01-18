using Onlooker.IntermediateConfiguration.Common.Fonts;
using Onlooker.IntermediateConfiguration.Common.Graphics;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration.Common;

public class CommonConfig : ConfigGroup
{
    [ConfigLocation("configuration/common/graphics")] public GraphicsConfigGroup Graphics { get; }
    [ConfigLocation("configuration/common/fonts")] public FontsConfigGroup Fonts { get; }

    public CommonConfig()
    {
        Graphics = new GraphicsConfigGroup();
        Fonts = new FontsConfigGroup();
    }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        base.UpdateFromDirectory(root, progress);
        
        return;
        
        Graphics.UpdateFromDirectory(root.CreateSubdirectory("graphics"), progress);
        Fonts.UpdateFromDirectory(root.CreateSubdirectory("fonts"), progress);
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        Graphics.WriteToDirectory(root.CreateSubdirectory("graphics"), progress);
        Fonts.WriteToDirectory(root.CreateSubdirectory("fonts"), progress);
    }
}
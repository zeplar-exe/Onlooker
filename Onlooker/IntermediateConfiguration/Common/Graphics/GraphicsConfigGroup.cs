using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Common.Graphics;

public class GraphicsConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/common/graphics/loading_screen.png")]
    public Texture2D? LoadingScreen { get; set; }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        using var stream = File.OpenRead(Path.Join(Directory.GetCurrentDirectory(),
            "configuration/common/graphics/loading_screen.png"));
        
        LoadingScreen?.SaveAsPng(stream, LoadingScreen.Width, LoadingScreen.Height);
    }
}
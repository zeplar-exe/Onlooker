using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Common.Graphics;

public class GraphicsConfigGroup : ConfigGroup
{
    public Texture2D? LoadingScreen { get; set; }
    
    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        LoadingScreen = Texture2D.FromFile(
            GameManager.Current.GraphicsDevice, 
            Path.Join(Directory.GetCurrentDirectory(), "configuration/common/graphics/loading_screen.png"));
        
        // TODO: Ok, I really need a way to remove this boilerplate and stuff
        progress.Report(new ConfigUpdateStatus("Loaded 'common/graphics/loading_screen.png'", UpdateStatusType.Success));
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        using var stream = File.OpenRead(Path.Join(Directory.GetCurrentDirectory(),
            "configuration/common/graphics/loading_screen.png"));
        
        LoadingScreen?.SaveAsPng(stream, LoadingScreen.Width, LoadingScreen.Height);
    }
}
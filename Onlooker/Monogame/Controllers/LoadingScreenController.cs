using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.IntermediateConfiguration;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.Monogame.Controllers;

public class LoadingScreenController : GameController
{
    private string? LastMessage { get; set; }
    private Progress<ConfigUpdateStatus> Progress { get; }

    public LoadingScreenController()
    {
        Progress = new Progress<ConfigUpdateStatus>();
    }

    public async Task Load(CancellationToken token)
    {
        Progress.ProgressChanged += (_, status) => LastMessage = $"{status.Type}: {status.Message}";

        GameManager.Current.Configuration.UpdateFromDirectory(
            new DirectoryInfo(Path.Join(Directory.GetCurrentDirectory(), "configuration")), Progress);
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        var rect = GameManager.Current.GraphicsDevice.PresentationParameters.Bounds;

        canvas.Draw(0, new TextureItem(GameManager.Current.Configuration.CommonConfig.Graphics.LoadingScreen!, rect, Color.Black));

        canvas.Draw(0, new StringItem(new StringBuilder(LastMessage), 
            GameManager.Current.Configuration.CommonConfig.Fonts.Information!, rect, Color.White));
    }
}
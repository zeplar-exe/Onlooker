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

    public void Load()
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
        var screenRect = GameManager.Current.GraphicsDevice.PresentationParameters.Bounds;

        canvas.Draw(0, 
            new TextureItem(
                GameManager.Current.Configuration.CommonConfig.Graphics.LoadingScreen!,
                screenRect, Color.Black));

        var text = new StringBuilder(LastMessage);
        var font = GameManager.Current.Configuration.CommonConfig.Fonts.Information!;
        var (fontX, fontY) = font.MeasureString(text);
        var halfFontOffset = new Point((int)(fontX / 2), (int)(fontY / 2));
        
        var centeredRect = new Rectangle(
            screenRect.Center - halfFontOffset, 
            new Point(screenRect.Size.X / 2, screenRect.Size.Y / 2));

        canvas.Draw(0, new StringItem(new StringBuilder(LastMessage), 
            font, centeredRect, Color.White));
    }
}
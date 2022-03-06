using System.Text;
using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.IntermediateConfiguration.Game;
using Onlooker.Monogame.Graphics;
using Onlooker.Monogame.Logging;

namespace Onlooker.Monogame.Controllers;

public class LoadingScreenController : GameController
{
    private string? LastMessage { get; set; }
    private Progress<ConfigUpdateStatus> Progress { get; }
    
    public bool LoadingCompleted { get; private set; }

    public LoadingScreenController()
    {
        Progress = new Progress<ConfigUpdateStatus>();
    }

    public async void Load()
    {
        Progress.ProgressChanged += OnLoadProgress;

        await Task.Run(delegate
        {
            GameManager.Current.Configuration.UpdateFromDirectory(
                new DirectoryInfo(Path.Join(Directory.GetCurrentDirectory(), "configuration")), Progress);
        });
    }

    private void OnLoadProgress(object? _, ConfigUpdateStatus status)
    {
        LastMessage = status.Message;
        GameManager.Current.Logger.Log(AppLogger.LoadingLog, LogMessageBuilder.TimestampedMessage(LastMessage));

        if (status.Type == UpdateStatusType.Completed)
        {
            LoadingCompleted = true;
            Progress.ProgressChanged -= OnLoadProgress;
        }
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        // Null coalesce is required to prevent errors while config groups have not yet loaded
        var texture = GameManager.Current.Configuration?.CommonConfig?.Graphics?.LoadingScreen;
        var font = GameManager.Current.Configuration?.CommonConfig?.Fonts?.Information;

        if (texture == null)
            return;
        
        canvas.Draw(Layers.PriorityUI, new TextureGraphic(texture, CommonValues.ScreenRect));

        if (font != null)
        {
            var (xCenter, yCenter) = new Point(
                Math2.FloorToInt(CommonValues.ScreenWidth.X / 2), 
                Math2.FloorToInt(CommonValues.ScreenHeight.Y / 2));
            var (textX, textY) = font.MeasureString(LastMessage);
            
            var offsetCenter = new Point(
                Math2.FloorToInt(xCenter - textX / 2), 
                Math2.FloorToInt(yCenter - textY / 2));
            
            var centeredRect = new Rectangle(offsetCenter, new Point((int)textX, (int)textY));
            
            canvas.Draw(Layers.PriorityUI, new StringGraphic(new StringBuilder(LastMessage), font, centeredRect));
        }
    }

    public override bool IsLocked()
    {
        return false;
    }
}
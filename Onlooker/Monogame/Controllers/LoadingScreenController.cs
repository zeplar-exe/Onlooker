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
        LastMessage = $"{status.Type}, {status.Message}";
        GameManager.Current.Logger.Log(AppLogger.ConfigLog, LogMessageBuilder.TimestampedMessage(LastMessage));

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

        if (texture == null)
            return;
        
        canvas.Draw(0, new TextureGraphic(texture, CommonValues.ScreenRect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
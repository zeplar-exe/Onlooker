using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.IntermediateConfiguration.Game;

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

    public void Load()
    {
        Progress.ProgressChanged += OnLoadProgress;

        GameManager.Current.Configuration.UpdateFromDirectory(
            new DirectoryInfo(Path.Join(Directory.GetCurrentDirectory(), "configuration")), Progress);
    }

    private void OnLoadProgress(object? _, ConfigUpdateStatus status)
    {
        LastMessage = $"{status.Type}: {status.Message}";

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
        var texture = GameManager.Current.Configuration.CommonConfig.Graphics.LoadingScreen;

        if (texture == null)
            return;
        
        canvas.Draw(0, 
            new TextureItem(
                texture,
                CommonValues.ScreenRect, Color.Black));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
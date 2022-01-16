using Microsoft.Xna.Framework;

namespace Onlooker.Monogame.Controllers;

public class MainController : GameController
{
    private LoadingScreenController LoadingScreen { get; }
    
    public MainController()
    {
        LoadingScreen = new LoadingScreenController();
        
        GameManager.Current.HookController(LoadingScreen);
    }

    public override async void OnContentLoad()
    {
        await LoadingScreen.Load(CancellationToken.None);
        
        base.OnContentLoad();
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }
}
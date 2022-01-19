using Jammo.ParserTools.Tools;
using Microsoft.Xna.Framework;
using Onlooker.Common;

namespace Onlooker.Monogame.Controllers;

public class MainController : GameController
{
    private StateMachine<GameState> State { get; }
    private LoadingScreenController LoadingScreen { get; }
    private MainMenuController MainMenu { get; }
    
    public MainController()
    {
        State = new StateMachine<GameState>(GameState.None);
        
        LoadingScreen = new LoadingScreenController { Enabled = true };
        MainMenu = new MainMenuController();
        
        GameManager.Current.HookController(LoadingScreen);
    }

    public override void OnContentLoad()
    {
        LoadingScreen.Load();
        
        base.OnContentLoad();
    }

    public override void Update(GameTime time)
    {
        Time.Delta = time;
        
        switch (State.Current)
        {
            case GameState.None:
                State.MoveTo(GameState.LoadingStarted);

                goto case GameState.LoadingStarted;
            case GameState.LoadingStarted:
                LoadingScreen.Load();
                
                State.MoveTo(GameState.LoadingUpdating);

                break;
            case GameState.LoadingUpdating:
                if (LoadingScreen.LoadingCompleted)
                {
                    LoadingScreen.Enabled = false;
                    State.MoveTo(GameState.MainMenuStarted);
                    
                    goto case GameState.MainMenuStarted;
                }
                
                break;
            case GameState.MainMenuStarted:
                MainMenu.Enabled = true;

                var transition = new DirectionalFadeTransitionController(
                    new DirectionalFadeTransitionOptions(
                        Color.Aqua, TimeSpan.FromSeconds(0.5),
                        Direction.Down, TransitionEndHandler.Disable))
                {
                    ZIndex = 1,
                    Enabled = true
                };
                
                GameManager.Current.HookController(transition);

                transition.Start();

                State.MoveTo(GameState.MainMenuUpdating);
                
                break;
            case GameState.MainMenuUpdating:
                break;
            case GameState.WorldStarted:
                break;
            case GameState.WorldUpdating:
                break;
        }
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }
    
    public override bool IsLocked()
    {
        return false;
    }
}
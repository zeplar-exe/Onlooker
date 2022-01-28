using Jammo.ParserTools.Tools;
using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Monogame.Controllers.Transitions;
using Onlooker.ObjectProperties.Animation;

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
        Time.LastUpdate = time;
        
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
                    LoadingScreen.Disposed = true;
                    State.MoveTo(GameState.MainMenuStarted);
                    
                    goto case GameState.MainMenuStarted;
                }
                
                break;
            case GameState.MainMenuStarted:
                MainMenu.Enabled = true;

                var transition = new FadeController(1, Color.DarkGray)
                {
                    Enabled = true,
                    CompletionHandler = ControllerCompletionHandler.Dispose
                };
                
                GameManager.Current.HookController(transition);
                GameManager.Current.HookController(MainMenu);

                transition.QueueFillScreen(TransitionFillDirection.TopToBottom, new AnimationSettings
                {
                    Length = TimeSpan.FromSeconds(3),
                    Interval = TimeSpan.FromSeconds(0.1)
                });
                
                transition.QueueFillScreen(TransitionFillDirection.ToBottomClear, new AnimationSettings
                {
                    Length = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(0.1)
                });
                
                transition.PlayAllBatches();

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
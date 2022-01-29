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

    public override void Update(GameTime time)
    {
        Time.LastUpdate = time;
        
        switch (State.Current)
        {
            case GameState.None:
                State.MoveTo(GameState.LoadingStarted);

                goto case GameState.LoadingStarted;
            case GameState.LoadingStarted:
                LoadingScreen.Load(); // by the time update runs, graphics devices have been initialized
                
                State.MoveTo(GameState.LoadingUpdating);

                break;
            case GameState.LoadingUpdating:
                if (LoadingScreen.LoadingCompleted)
                {
                    State.MoveTo(GameState.MainMenuStarted);
                    
                    goto case GameState.MainMenuStarted;
                }
                
                break;
            case GameState.MainMenuStarted:
                var transition = new FadeController(1, Color.DarkGray)
                {
                    Enabled = true,
                    CompletionHandler = ControllerCompletionHandler.Dispose
                };
                
                GameManager.Current.HookController(transition);
                GameManager.Current.HookController(MainMenu);

                var fill = transition.QueueFillScreen(TransitionFillDirection.TopToBottom, new AnimationSettings
                {
                    Length = TimeSpan.FromSeconds(3),
                    Interval = TimeSpan.FromSeconds(0.1)
                });
                
                fill.Animator.Completed += (_, _) =>
                {
                    MainMenu.Enabled = true;
                    LoadingScreen.Disposed = true;
                };
                
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
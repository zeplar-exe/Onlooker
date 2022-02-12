using Jammo.ParserTools.Tools;
using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Generation;
using Onlooker.Monogame.Controllers.Transitions;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers;

public class MainController : GameController
{
    private LoadingScreenController LoadingScreen { get; }
    private MainMenuController MainMenu { get; }
    private RandomMapController RandomMap { get; }
    
    public StateMachine<GameState> State { get; }
    
    public MainController()
    {
        State = new StateMachine<GameState>(GameState.None);
        
        LoadingScreen = new LoadingScreenController { Enabled = true };
        MainMenu = new MainMenuController();
        RandomMap = new RandomMapController();
        
        GameManager.Current.HookController(LoadingScreen);
    }

    public override void Update(GameTime time)
    {
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
                    LoadingScreen.Enabled = false;
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
                MainMenu.Enabled = false;

                var noise = new NoiseGenerator();
                noise.Frequencies.Add(new NoiseFrequency(1, 1));
                
                RandomMap.Generate(new Vector2Int(75, 75), noise);
                // TODO: Take pointers from that unity project to deal with image squash/stretching
                RandomMap.Enabled = true;
                
                GameManager.Current.HookController(RandomMap);
                
                State.MoveTo(GameState.WorldUpdating);
                
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
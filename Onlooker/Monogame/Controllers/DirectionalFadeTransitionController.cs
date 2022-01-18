using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.ObjectProperties;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers;

public class DirectionalFadeTransitionController : GameController
{
    private Animator<int>? Animator { get; set; }
    private IntegerProperty ScreenProgress { get; }
    private DirectionalFadeTransitionOptions Options { get; }
    
    public bool IsActive { get; private set; }
    public int ZIndex { get; set; }

    public DirectionalFadeTransitionController(DirectionalFadeTransitionOptions options)
    {
        ScreenProgress = new IntegerProperty(0);
        Options = options;
    }

    public async void Start()
    {
        IsActive = true;
        Animator = ScreenProgress.Animate(CommonValues.ScreenRect.Height, new AnimationSettings
        {
            Type = AnimationType.Linear,
            Length = Options.Length
        });
        
        Animator.Completed += delegate
        {
            IsActive = false;

            switch (Options.EndHandler)
            {
                case TransitionEndHandler.Disable:
                    Enabled = false;
                    break;
                case TransitionEndHandler.Dispose:
                    Disposed = false;
                    break;
            }
        };

        await Animator.Start(CancellationToken.None);
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        var rectangle = Rectangle.Empty;

        switch (Options.Direction)
        {
            case Direction.Up:
                rectangle = new Rectangle(
                    0, CommonValues.ScreenRect.Y - ScreenProgress, 
                    CommonValues.ScreenRect.Width, ScreenProgress);
                break;
            case Direction.Down:
                rectangle = new Rectangle(
                    0,  0,  
                    CommonValues.ScreenRect.Width, ScreenProgress);
                break;
            case Direction.Left:
                rectangle = new Rectangle(
                    CommonValues.ScreenRect.X,  0,  
                    CommonValues.ScreenRect.Width - ScreenProgress, CommonValues.ScreenRect.Height);
                break;
            case Direction.Right:
                rectangle = new Rectangle(
                    0,  0,  
                    ScreenProgress, CommonValues.ScreenRect.Height);
                break;
        }
        
        canvas.Draw(ZIndex,
            new TextureItem(GameManager.Current.Configuration.CommonConfig.Graphics.Black!,
                rectangle, Options.Color));
    }

    public override bool IsLocked()
    {
        return false;
    }
}

public class DirectionalFadeTransitionOptions
{
    public Color Color { get; set; }
    public TimeSpan Length { get; set; }
    public Direction Direction { get; set; }
    public TransitionEndHandler EndHandler { get; set; }
    
    public DirectionalFadeTransitionOptions(Color color, 
        TimeSpan length,
        Direction direction, 
        TransitionEndHandler endHandler)
    {
        Color = color;
        Length = length;
        Direction = direction;
        EndHandler = endHandler;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum TransitionEndHandler
{
    None = 0,
    
    Disable,
    Dispose
}
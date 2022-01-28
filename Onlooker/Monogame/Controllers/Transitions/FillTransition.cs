using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers.Transitions;

public class FillTransition : FadeTransition
{
    private TransitionFillDirection Direction { get; }
    public Animator<Rectangle> Animator { get; }

    public FillTransition(SolidColorController displayRectangle, TransitionFillDirection direction, AnimationSettings settings) 
        : base(displayRectangle, settings)
    {
        Direction = direction;
        
        var result = Rectangle.Empty;
        
        switch (Direction)
        {
            case TransitionFillDirection.CenterToBorders:
                break;
            case TransitionFillDirection.CurrentToRight:
                break;
            case TransitionFillDirection.CurrentToLeft:
                break;
            case TransitionFillDirection.CurrentToTop:
                break;
            case TransitionFillDirection.CurrentToBottom:
                break;
            case TransitionFillDirection.ToRightClear:
                break;
            case TransitionFillDirection.ToLeftClear:
                break;
            case TransitionFillDirection.ToTopClear:
                break;
            case TransitionFillDirection.ToBottomClear:
                result = new Rectangle(CommonValues.ScreenHeight, CommonValues.ScreenSize);
                break;
            case TransitionFillDirection.RightToLeft:
                break;
            case TransitionFillDirection.LeftToRight:
                break;
            case TransitionFillDirection.TopToBottom:
                result = CommonValues.ScreenRect;
                
                DisplayRectangle.Rectangle.Value = new Rectangle(
                    new Point(0, 0), 
                    new Point(result.Width, 0));
                break;
            case TransitionFillDirection.BottomToTop:
                break;
        }

        Animator = DisplayRectangle.Rectangle.Animate(result, Settings);
    }

    public override bool TryStep()
    {
        return !Animator.Step().Completed;
    }
}
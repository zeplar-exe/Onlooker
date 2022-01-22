namespace Onlooker.Monogame.Controllers.Transitions;

public enum TransitionFillDirection
{
    None = 0,
    
    CenterToBorders,
    
    CurrentToRight,
    CurrentToLeft,
    CurrentToTop,
    CurrentToBottom,
    
    ToRightClear,
    ToLeftClear,
    ToTopClear,
    ToBottomClear,
    
    RightToLeft,
    LeftToRight,
    TopToBottom,
    BottomToTop,
}
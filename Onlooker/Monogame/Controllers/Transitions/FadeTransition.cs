using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers.Transitions;

public abstract class FadeTransition
{
    protected SolidColorController DisplayRectangle { get; }
    protected AnimationSettings Settings { get; }

    public FadeTransition(SolidColorController displayRectangle, AnimationSettings settings)
    {
        DisplayRectangle = displayRectangle;
        Settings = settings;
    }

    public abstract bool TryStep();
}
using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers.Transitions;

public class ColorTransition : FadeTransition
{
    private Color Color { get; }
    public Animator<Color> Animator { get; private set; }

    public ColorTransition(SolidColorController displayRectangle, Color color, AnimationSettings settings) 
        : base(displayRectangle, settings)
    {
        Color = color;
        Animator = DisplayRectangle.Color.Animate(Color, settings);
    }

    public override bool TryStep()
    {
        return !Animator.Step().Completed;
    }
}
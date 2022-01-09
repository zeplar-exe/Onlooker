using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class RectProperty : ObjectProperty<Rect>
{
    public RectProperty(Rect value) : base(value)
    {
        
    }

    public override Animator<Rect> Animate(Rect result, AnimationSettings settings)
    {
        var bottomLeft = new Vector2Property(Value.BottomLeft).Animate(result.BottomLeft, settings).GetPropertySequence().ToArray();
        var size = new Vector2Property(Value.Size).Animate(result.Size, settings).GetPropertySequence().ToArray();

        var values = bottomLeft.Select((_, i) => new Rect(bottomLeft[i], size[i])).ToArray();

        return new Animator<Rect>(this, values, settings.Length.TotalSeconds / values.Length);
    }
}
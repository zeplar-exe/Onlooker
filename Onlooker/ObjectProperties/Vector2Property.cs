using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class Vector2Property : ObjectProperty<Vector2>
{
    public Vector2Property(Vector2 value) : base(value)
    {
        
    }

    public override Animator<Vector2> Animate(Vector2 result, AnimationSettings settings)
    {
        var x = new FloatProperty(Value.X).Animate(result.X, settings).GetPropertySequence().ToArray();
        var y = new FloatProperty(Value.Y).Animate(result.Y, settings).GetPropertySequence().ToArray();

        var values = x.Select((_, i) => new Vector2(x[i], y[i])).ToArray();

        return new Animator<Vector2>(this, values, settings.Length.TotalSeconds / values.Length);
    }
}
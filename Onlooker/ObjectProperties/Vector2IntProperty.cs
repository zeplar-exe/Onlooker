using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class Vector2IntProperty : ObjectProperty<Vector2Int>
{
    public Vector2IntProperty(Vector2Int value) : base(value)
    {
        
    }

    public override Animator<Vector2Int> Animate(Vector2Int result, AnimationSettings settings)
    {
        var x = new IntegerProperty(Value.X).Animate(result.X, settings).GetPropertySequence().ToArray();
        var y = new IntegerProperty(Value.Y).Animate(result.Y, settings).GetPropertySequence().ToArray();

        var values = x.Select((_, i) => new Vector2Int(x[i], y[i])).ToArray();

        return new Animator<Vector2Int>(this, values, settings.Length.TotalSeconds / values.Length);
    }
}
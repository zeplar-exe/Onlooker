using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class Vector2Property : ObjectProperty<Vector2>
{
    public Vector2Property(Vector2 value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(Vector2 start, Vector2 end, AnimationSettings settings, out Vector2 next)
    {
        next = end;
        
        if (Value == end)
            return false;
        
        var x = new FloatProperty(Value.X);
        var y = new FloatProperty(Value.Y);

        x.TryCreateNextFrame(start.X, end.X, settings, out var xNext);
        y.TryCreateNextFrame(start.Y, end.Y, settings, out var yNext);
        
        next = new Vector2(xNext, yNext);

        return true;
    }
}
using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class Vector2IntProperty : ObjectProperty<Vector2Int>
{
    public Vector2IntProperty(Vector2Int value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(Vector2Int start, Vector2Int end, AnimationSettings settings, out Vector2Int next)
    {
        next = Value;
        
        if (Value == end)
            return false;
        
        var x = new IntegerProperty(Value.X);
        var y = new IntegerProperty(Value.Y);

        x.TryCreateNextFrame(start.X, end.X, settings, out var xNext);
        y.TryCreateNextFrame(start.Y, end.Y, settings, out var yNext);
        
        next = new Vector2Int(xNext, yNext);

        return true;
    }
}
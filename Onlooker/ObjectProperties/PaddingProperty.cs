using Onlooker.Common._2D;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class PaddingProperty : ObjectProperty<Padding>
{

    public PaddingProperty(Padding value) : base(value)
    {
    }

    protected internal override bool TryCreateNextFrame(Padding start, Padding end, AnimationSettings settings, out Padding next)
    {
        next = Value;

        if (Value == end)
            return false;
        
        new IntegerProperty(Value.Up).TryCreateNextFrame(start.Up, end.Up, settings, out var up);
        new IntegerProperty(Value.Down).TryCreateNextFrame(start.Down, end.Down, settings, out var down);
        new IntegerProperty(Value.Left).TryCreateNextFrame(start.Left, end.Left, settings, out var left);
        new IntegerProperty(Value.Right).TryCreateNextFrame(start.Right, end.Right, settings, out var right);

        next = new Padding(up, down, left, right);
        
        return true;
    }
}
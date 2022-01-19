using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class RectProperty : ObjectProperty<Rect>
{
    public RectProperty(Rect value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(Rect start, Rect end, AnimationSettings settings, out Rect next)
    {
        next = default;

        if (Value == end)
            return false;

        new Vector2Property(Value.BottomLeft).TryCreateNextFrame(start.BottomLeft, end.BottomLeft, settings, out var blNext);
        new Vector2Property(Value.BottomLeft).TryCreateNextFrame(start.Size, end.Size, settings, out var sNext);

        next = new Rect(blNext, sNext);
        
        return true;
    }
}
using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class BooleanProperty : ObjectProperty<bool>
{
    public BooleanProperty(bool value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(bool start, bool end, AnimationSettings settings, out bool next)
    {
        next = Value;

        if (start == end)
            return false;

        new IntegerProperty(start.AsInt()).TryCreateNextFrame(0, 1, settings, out var i);

        next = i.AsBool();

        return true;
    }
}
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class TimeSpanProperty : ObjectProperty<TimeSpan>
{
    public TimeSpanProperty(TimeSpan value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(TimeSpan start, TimeSpan end, AnimationSettings settings, out TimeSpan next)
    {
        next = default;
        
        if (Value == end)
            return false;
        
        var seconds = new DoubleProperty(Value.TotalSeconds);

        seconds.TryCreateNextFrame(start.TotalSeconds, end.TotalSeconds, settings, out var sNext);
        
        next = TimeSpan.FromSeconds(sNext);

        return true;
    }
}
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class TimeSpanProperty : ObjectProperty<TimeSpan>
{
    public TimeSpanProperty(TimeSpan value) : base(value)
    {
        
    }
    
    public override Animator<TimeSpan> Animate(TimeSpan result, AnimationSettings settings)
    {
        var seconds = new DoubleProperty(Value.TotalSeconds).Animate(result.TotalSeconds, settings);
        var values = seconds.GetPropertySequence().ToArray();
        
        return new Animator<TimeSpan>(this, 
            values.Select(TimeSpan.FromSeconds), 
            settings.Length.TotalSeconds / values.Length);
    }
}
using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class FloatProperty : ObjectProperty<float>
{
    private const float ComparisonTolerance = 0.00000000001f;
    
    public FloatProperty(float value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(float start, float end, AnimationSettings settings, out float next)
    {
        next = end;
        
        if (Math.Abs(Value - end) < ComparisonTolerance)
            return false;
        
        switch (settings.Type)
        {
            case AnimationType.LinearConstant:
            {
                var minAlpha = Time.Delta.TotalSeconds * settings.Length.TotalSeconds;
                settings.Alpha += minAlpha;

                next = Math.Min(Math2.Lerp(Value, end, settings.Alpha), end);
                
                return true;
            }
        }

        return false;
    }
}
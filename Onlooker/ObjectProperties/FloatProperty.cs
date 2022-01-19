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
        next = 0;
        
        if (Math.Abs(Value - end) > ComparisonTolerance)
            return false;
        
        switch (settings.Type)
        {
            case AnimationType.Linear:
            {
                var invAlpha = Math2.InvLerp(start, end, Value);
                // TODO: Gotta get rid of this magic number (0.1)
                var minAlpha = 0.1 * (1 / settings.Length.TotalSeconds);
                var alpha = invAlpha + minAlpha;

                next = Math.Min(Math2.Lerp(Value, end, alpha), end);
                
                return true;
            }
        }

        return false;
    }
}
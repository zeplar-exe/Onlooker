using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class DoubleProperty : ObjectProperty<double>
{
    private const double ComparisonTolerance = 0.0000000000000001d;
    
    public DoubleProperty(double value) : base(value)
    {
        
    }
    
    protected internal override bool TryCreateNextFrame(double start, double end, AnimationSettings settings, out double next)
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
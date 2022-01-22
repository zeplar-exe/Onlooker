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
        next = end;
        
        if (Math.Abs(Value - end) < ComparisonTolerance)
            return false;
        
        switch (settings.Type)
        {
            case AnimationType.LinearConstant:
            {
                var minAlpha = 1 / settings.Length.TotalSeconds;
                settings.Alpha += minAlpha;

                next = Math.Min(Math2.Lerp(Value, end, settings.Alpha), end);
                
                return true;
            }
        }

        return false;
    }
}
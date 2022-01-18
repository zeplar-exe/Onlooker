using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class DoubleProperty : ObjectProperty<double>
{
    private const double ComparisonTolerance = 0.0000000000000001d;
    
    public DoubleProperty(double value) : base(value)
    {
        
    }
    
    public override Animator<double> Animate(double result, AnimationSettings settings)
    {
        if (Math.Abs(Value - result) < ComparisonTolerance)
            return CreateEmptyAnimator();
        
        var values = new List<double>();

        switch (settings.Type)
        {
            case AnimationType.Linear:
            {
                var minAlpha = 0.1 * (1 / settings.Length.TotalSeconds);
                var alpha = 0d;

                while ((alpha += minAlpha) < 1)
                {
                    values.Add(Math2.Lerp(Value, result, alpha));
                }
        
                if (Math.Abs(values.Last() - result) > ComparisonTolerance)
                    values.Add(result);
                
                break;
            }
        }

        return new Animator<double>(this, values, settings.Length.TotalSeconds / values.Count);
    }
}
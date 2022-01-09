using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class FloatProperty : ObjectProperty<float>
{
    private const float ComparisonTolerance = 0.00000000001f;
    
    public FloatProperty(float value) : base(value)
    {
        
    }

    public override Animator<float> Animate(float result, AnimationSettings settings)
    {
        if (Math.Abs(Value - result) < ComparisonTolerance)
            return CreateEmptyAnimator();
        
        var values = new List<float>();

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

        return new Animator<float>(this, values, settings.Length.TotalSeconds / values.Count);
    }
}
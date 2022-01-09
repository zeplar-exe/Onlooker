using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class IntegerProperty : ObjectProperty<int>
{
    public IntegerProperty(int value) : base(value)
    {
        
    }

    public override Animator<int> Animate(int result, AnimationSettings settings)
    {
        if (Value == result)
            return CreateEmptyAnimator();
        
        var values = new List<int>();

        switch (settings.Type)
        {
            case AnimationType.Linear:
            {
                // TODO: Gotta get rid of this magic number (0.1)
                var minAlpha = 0.1 * (1 / settings.Length.TotalSeconds);
                var alpha = 0d;

                while ((alpha += minAlpha) < 1)
                {
                    values.Add(Math2.Lerp(Value, result, alpha));
                }
        
                if (values.Last() != result)
                    values.Add(result);
                
                break;
            }
        }

        return new Animator<int>(this, values, settings.Length.TotalSeconds / values.Count);
    }
}
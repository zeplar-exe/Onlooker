using Onlooker.Common;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class IntegerProperty : ObjectProperty<int>
{
    public IntegerProperty(int value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(int start, int end, AnimationSettings settings, out int next)
    {
        next = 0;
        
        if (Value == end)
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
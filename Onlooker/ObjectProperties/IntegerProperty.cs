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
        next = end;
        
        if (Value == end)
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
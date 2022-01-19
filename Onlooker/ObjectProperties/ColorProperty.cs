using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class ColorProperty : ObjectProperty<Color>
{
    public ColorProperty(Color value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(Color start, Color end, AnimationSettings settings, out Color next)
    {
        next = default;

        if (Value == end)
            return false;
        
        new IntegerProperty(Value.A).TryCreateNextFrame(start.A, end.A, settings, out var aNext);
        new IntegerProperty(Value.R).TryCreateNextFrame(start.A, end.A, settings, out var rNext);
        new IntegerProperty(Value.G).TryCreateNextFrame(start.A, end.A, settings, out var gNext);
        new IntegerProperty(Value.B).TryCreateNextFrame(start.A, end.A, settings, out var bNext);

        next = new Color(aNext, rNext, gNext, bNext);

        return true;
    }
}
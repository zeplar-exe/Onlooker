using Microsoft.Xna.Framework;
using Onlooker.Common.Extensions;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.ObjectProperties;

public class RectangleProperty : ObjectProperty<Rectangle>
{
    public RectangleProperty(Rectangle value) : base(value)
    {
        
    }

    protected internal override bool TryCreateNextFrame(Rectangle start, Rectangle end, AnimationSettings settings, out Rectangle next)
    {
        next = Value;

        if (Value == end)
            return false;

        new Vector2Property(Value.TopLeft()).TryCreateNextFrame(start.TopLeft(), end.TopLeft(), settings, out var position);
        new Vector2Property(Value.Size).TryCreateNextFrame(start.Size, end.Size, settings, out var size);

        next = new Rectangle(position, size);
        
        return true;
    }
}
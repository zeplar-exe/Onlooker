using Microsoft.Xna.Framework;
using Vector2 = Onlooker.Common._2D.Vector2;

namespace Onlooker.Common.Extensions;

public static class RectangleExtensions
{
    public static Vector2 BottomLeft(this Rectangle rectangle)
    {
        return new Vector2(rectangle.Left, rectangle.Bottom);
    }
    
    public static Vector2 TopLeft(this Rectangle rectangle)
    {
        return new Vector2(rectangle.Left, rectangle.Top);
    }
    
    public static Vector2 BottomRight(this Rectangle rectangle)
    {
        return new Vector2(rectangle.Right, rectangle.Bottom);
    }
    public static Vector2 TopRight(this Rectangle rectangle)
    {
        return new Vector2(rectangle.Right, rectangle.Top);
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Onlooker.Common;

public static class MouseHelper
{
    public static bool IsOverRect(Rectangle rectangle)
    {
        return rectangle.Contains(Mouse.GetState().Position);
    }

    public static bool IsMousePressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Mouse.GetState().LeftButton == ButtonState.Pressed;
    }

    public static bool IsMouseReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Mouse.GetState().LeftButton == ButtonState.Released;
    }
}
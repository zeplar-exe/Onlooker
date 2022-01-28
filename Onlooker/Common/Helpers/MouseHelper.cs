using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Onlooker.Common.Helpers;

public static class MouseHelper
{
    public static bool IsOverRect(Rectangle rectangle)
    {
        return rectangle.Contains(Mouse.GetState().Position);
    }

    public static bool IsLeftButtonPressed()
    {
        return Mouse.GetState().LeftButton == ButtonState.Pressed;
    }

    public static bool IsLeftButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && IsLeftButtonPressed();
    }
    
    public static bool IsLeftButtonReleased()
    {
        return Mouse.GetState().LeftButton == ButtonState.Released;
    }

    public static bool IsLeftButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && IsLeftButtonReleased();
    }
    
    
    public static bool IsRightButtonPressed()
    {
        return Mouse.GetState().RightButton == ButtonState.Pressed;
    }

    public static bool IsRightButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && IsRightButtonPressed();
    }
    
    public static bool IsRightButtonReleased()
    {
        return Mouse.GetState().RightButton == ButtonState.Released;
    }

    public static bool IsRightButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && IsRightButtonReleased();
    }
}
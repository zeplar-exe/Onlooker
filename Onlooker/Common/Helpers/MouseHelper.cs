using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Onlooker.Monogame;

namespace Onlooker.Common.Helpers;

public static class MouseHelper
{
    public static bool IsOverRect(Rectangle rectangle)
    {
        return rectangle.Contains(Mouse.GetState().Position);
    }

    public static bool IsLeftButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsLeftMousePressed();
    }
    
    public static bool IsLeftButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsLeftMouseHeld();
    }

    public static bool IsLeftButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsLeftMouseReleased();
    }
    
    public static bool IsRightButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsRightMousePressed();
    }
    
    public static bool IsRightButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsRightMouseHeld();
    }

    public static bool IsRightButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsRightMouseReleased();
    }
    
    public static bool IsMiddleButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsMiddleMousePressed();
    }
    
    public static bool IsMiddleButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsMiddleMouseHeld();
    }

    public static bool IsMiddleButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && GameManager.Current.Input.IsMiddleMouseReleased();
    }
}
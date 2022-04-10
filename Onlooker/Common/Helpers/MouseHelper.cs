using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Onlooker.Monogame;
using Onlooker.Monogame.Controllers;

namespace Onlooker.Common.Helpers;

public static class MouseHelper
{
    private static InputFrameworkController Input => GameManager.Current.Input;
    
    public static bool IsOverRect(Rectangle rectangle)
    {
        return rectangle.Contains(Mouse.GetState().Position);
    }

    public static bool IsLeftButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsLeftMousePressed();
    }
    
    public static bool IsLeftButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsLeftMouseHeld();
    }

    public static bool IsLeftButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsLeftMouseReleased();
    }
    
    public static bool IsRightButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsRightMousePressed();
    }
    
    public static bool IsRightButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsRightMouseHeld();
    }

    public static bool IsRightButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsRightMouseReleased();
    }
    
    public static bool IsMiddleButtonPressedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsMiddleMousePressed();
    }
    
    public static bool IsMiddleButtonHeldOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsMiddleMouseHeld();
    }

    public static bool IsMiddleButtonReleasedOverRect(Rectangle rectangle)
    {
        return IsOverRect(rectangle) && Input.IsMiddleMouseReleased();
    }
}
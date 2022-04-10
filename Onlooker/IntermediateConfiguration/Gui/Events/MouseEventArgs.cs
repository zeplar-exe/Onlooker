using Microsoft.Xna.Framework.Input;
using Onlooker.Common._2D;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.Gui.Events;

public class MouseEventArgs : EventArgs
{
    public Vector2Int WindowPosition { get; }
    public MouseButtonState LeftMouseButton { get; }
    public MouseButtonState RightMouseButton { get; }
    public MouseButtonState MiddleMouseButton { get; }

    public MouseEventArgs()
    {
        var input = GameManager.Current.Input;

        WindowPosition = Mouse.GetState().Position;

        if (input.IsLeftMousePressed())
            LeftMouseButton = MouseButtonState.Pressed;
        else if (input.IsLeftMouseHeld())
            LeftMouseButton = MouseButtonState.Held;
        else if (input.IsLeftMouseReleased())
            LeftMouseButton = MouseButtonState.Released;
        else
            LeftMouseButton = MouseButtonState.None;
        
        if (input.IsRightMousePressed())
            RightMouseButton = MouseButtonState.Pressed;
        else if (input.IsRightMouseHeld())
            RightMouseButton = MouseButtonState.Held;
        else if (input.IsRightMouseReleased())
            RightMouseButton = MouseButtonState.Released;
        else
            LeftMouseButton = MouseButtonState.None;
        
        if (input.IsMiddleMousePressed())
            MiddleMouseButton = MouseButtonState.Pressed;
        else if (input.IsMiddleMouseHeld())
            MiddleMouseButton = MouseButtonState.Held;
        else if (input.IsMiddleMouseReleased())
            MiddleMouseButton = MouseButtonState.Released;
        else
            MiddleMouseButton = MouseButtonState.None;
    }

    public MouseEventArgs(Vector2Int windowPosition, MouseButtonState leftMouseButton, MouseButtonState rightMouseButton, MouseButtonState middleMouseButton)
    {
        WindowPosition = windowPosition;
        LeftMouseButton = leftMouseButton;
        RightMouseButton = rightMouseButton;
        MiddleMouseButton = middleMouseButton;
    }
}

public enum MouseButtonState
{
    None = 0,
    
    Pressed,
    Held,
    Released
}

public enum RightMouseButtonState
{
    None = 0,
    
    Pressed,
    Held,
    Released
}

public enum MiddleMouseButtonState
{
    None = 0,
    
    Pressed,
    Held,
    Released
}
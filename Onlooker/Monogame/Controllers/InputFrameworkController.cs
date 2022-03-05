using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Onlooker.Common;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class InputFrameworkController : GameController
{
    private KeyboardState PreviousKeyboardState { get; set; }
    private KeyboardState CurrentKeyboardState { get; set; }
    
    private MouseState PreviousMouseState { get; set; }
    private MouseState CurrentMouseState { get; set; }

    public static InputFrameworkController Current => GameManager.Current.Input;

    public bool IsKeyPressed(Keys key)
    {
        return CurrentKeyboardState.GetPressedKeys().Contains(key) &&
               !PreviousKeyboardState.GetPressedKeys().Contains(key);
    }

    public bool IsKeyHeld(Keys key)
    {
        return CurrentKeyboardState.GetPressedKeys().Contains(key) &&
               PreviousKeyboardState.GetPressedKeys().Contains(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return !CurrentKeyboardState.GetPressedKeys().Contains(key) &&
               PreviousKeyboardState.GetPressedKeys().Contains(key);
    }

    public bool IsLeftMousePressed()
    {
        return CurrentMouseState.LeftButton == ButtonState.Pressed &&
               PreviousMouseState.LeftButton != ButtonState.Pressed;
    }
    
    public bool IsLeftMouseHeld()
    {
        return CurrentMouseState.LeftButton == ButtonState.Pressed &&
               PreviousMouseState.LeftButton == ButtonState.Pressed;
    }
    
    public bool IsLeftMouseReleased()
    {
        return CurrentMouseState.LeftButton != ButtonState.Pressed &&
               PreviousMouseState.LeftButton == ButtonState.Pressed;
    }
    
    public bool IsRightMousePressed()
    {
        return CurrentMouseState.RightButton == ButtonState.Pressed &&
               PreviousMouseState.RightButton != ButtonState.Pressed;
    }
    
    public bool IsRightMouseHeld()
    {
        return CurrentMouseState.RightButton == ButtonState.Pressed &&
               PreviousMouseState.RightButton == ButtonState.Pressed;
    }
    
    public bool IsRightMouseReleased()
    {
        return CurrentMouseState.RightButton != ButtonState.Pressed &&
               PreviousMouseState.RightButton == ButtonState.Pressed;
    }
    
    public bool IsMiddleMousePressed()
    {
        return CurrentMouseState.MiddleButton == ButtonState.Pressed &&
               PreviousMouseState.MiddleButton != ButtonState.Pressed;
    }
    
    public bool IsMiddleMouseHeld()
    {
        return CurrentMouseState.MiddleButton == ButtonState.Pressed &&
               PreviousMouseState.MiddleButton == ButtonState.Pressed;
    }
    
    public bool IsMiddleMouseReleased()
    {
        return CurrentMouseState.MiddleButton != ButtonState.Pressed &&
               PreviousMouseState.MiddleButton == ButtonState.Pressed;
    }
    
    public Vector2Int GetMouseDelta()
    {
        return CurrentMouseState.Position - PreviousMouseState.Position;
    }

    public Vector2Int GetScrollDelta()
    {
        return new Vector2Int(CurrentMouseState.HorizontalScrollWheelValue, CurrentMouseState.ScrollWheelValue) - 
               new Vector2Int(PreviousMouseState.HorizontalScrollWheelValue, PreviousMouseState.ScrollWheelValue);
    }

    public override void Update(GameTime time)
    {
        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = Keyboard.GetState();

        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = Mouse.GetState();
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }
}
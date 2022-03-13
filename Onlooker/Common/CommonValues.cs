using Microsoft.Xna.Framework;
using Onlooker.Monogame;
using Vector2 = Onlooker.Common._2D.Vector2;

namespace Onlooker.Common;

public static class CommonValues
{
    public static Rectangle ScreenRect => GameManager.Current.GraphicsDevice.PresentationParameters.Bounds;

    public static Vector2 ScreenSize => new Vector2(ScreenRect.Width, ScreenRect.Height);
    public static Vector2 ScreenWidth => new(ScreenRect.Width, 0);
    public static Vector2 ScreenHeight => new(0, ScreenRect.Height);
}
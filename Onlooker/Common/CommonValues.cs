using Microsoft.Xna.Framework;
using Onlooker.Monogame;

namespace Onlooker.Common;

public static class CommonValues
{
    public static Rectangle ScreenRect => GameManager.Current.GraphicsDevice.PresentationParameters.Bounds;
}
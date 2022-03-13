using Microsoft.Xna.Framework;
using Onlooker.Monogame;

namespace Onlooker.Common.Helpers;

public static class CoordinateConverter
{
    public static Rectangle ToScreenCoordinates(Rectangle rectangle)
    {
        if (GameManager.Current.PixelsPerCoordinate == 0)
            return Rectangle.Empty;

        return new Rectangle(
            rectangle.X,
            rectangle.Y,
            rectangle.Width * GameManager.Current.PixelsPerCoordinate,
            rectangle.Height * GameManager.Current.PixelsPerCoordinate);
    }

    public static Rectangle ToWorldCoordinates(Rectangle rectangle)
    {
        if (GameManager.Current.PixelsPerCoordinate == 0)
            return Rectangle.Empty;
        
        return new Rectangle(
            rectangle.X,
            rectangle.Y,
            rectangle.Width / GameManager.Current.PixelsPerCoordinate,
            rectangle.Height / GameManager.Current.PixelsPerCoordinate);
    }
}
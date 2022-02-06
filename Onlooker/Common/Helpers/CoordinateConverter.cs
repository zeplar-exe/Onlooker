using Microsoft.Xna.Framework;
using Onlooker.Monogame;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Common.Helpers;

public static class CoordinateConverter
{
    public static Rectangle ToScreenCoordinates(Rectangle rectangle)
    {
        if (GameManager.Current.PixelsPerCoordinate == 0)
            return Rectangle.Empty;

        return new Rectangle(
            rectangle.X / GameManager.Current.PixelsPerCoordinate,
            rectangle.Y / GameManager.Current.PixelsPerCoordinate,
            rectangle.Width / GameManager.Current.PixelsPerCoordinate,
            rectangle.Height / GameManager.Current.PixelsPerCoordinate);
    }

    public static Rectangle ToWorldCoordinates(Rectangle rectangle)
    {
        if (GameManager.Current.PixelsPerCoordinate == 0)
            return Rectangle.Empty;
        
        return new Rectangle(
            rectangle.X * GameManager.Current.PixelsPerCoordinate,
            rectangle.Y * GameManager.Current.PixelsPerCoordinate,
            rectangle.Width * GameManager.Current.PixelsPerCoordinate,
            rectangle.Height * GameManager.Current.PixelsPerCoordinate);
    }
}
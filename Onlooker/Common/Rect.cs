namespace Onlooker.Common;

public readonly record struct Rect(Vector2 BottomLeft, Vector2 Size)
{
    public Vector2 BottomRight => BottomLeft + new Vector2(Size.X, 0);
    public Vector2 TopLeft => BottomLeft + new Vector2(0, Size.Y);
    public Vector2 TopRight => BottomLeft + Size;

    public bool Contains(Vector2 point)
    {
        return point.X > BottomLeft.X && point.X < TopRight.X && point.Y > BottomLeft.Y && point.Y > TopRight.Y;
    }

    public bool Contains(Rect other)
    {
        return Contains(other.BottomLeft) &&
               Contains(other.BottomRight) &&
               Contains(other.TopLeft) &&
               Contains(other.TopRight);
    }
}
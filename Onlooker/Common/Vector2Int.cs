using Microsoft.Xna.Framework;

namespace Onlooker.Common;

public record struct Vector2Int(int X, int Y)
{
    public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    public Vector2Int Distance(Vector2Int other)
    {
        var relative = RelativeTo(other);

        return new Vector2Int(Math.Abs(relative.X), Math.Abs(relative.Y));
    }

    public Vector2Int RelativeTo(Vector2Int other)
    {
        return new Vector2Int(other.X - X, other.Y - Y);
    }

    public bool Equals(Vector2Int other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Vector2Int operator +(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X + right.X, left.Y + right.Y);
    }
    
    public static Vector2Int operator -(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X - right.X, left.Y - right.Y);
    }
    
    public static Vector2Int operator *(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X * right.X, left.Y * right.Y);
    }
    
    public static Vector2Int operator /(Vector2Int left, Vector2Int right)
    {
        return new Vector2Int(left.X / right.X, left.Y / right.Y);
    }
    
    public static implicit operator Vector2Int(Point point)
    {
        return new Vector2Int(point.X, point.Y);
    }
    
    public static implicit operator Point(Vector2Int vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }
    
    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}
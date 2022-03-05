using Microsoft.Xna.Framework;

namespace Onlooker.Common;

public record struct Vector2(float X, float Y)
{
    public double Magnitude => Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));

    public Vector2 Negate()
    {
        return new Vector2(-X, -Y);
    }
    
    public Vector2 Distance(Vector2 other)
    {
        var relative = RelativeTo(other);

        return new Vector2(Math.Abs(relative.X), Math.Abs(relative.Y));
    }

    public Vector2 RelativeTo(Vector2 other)
    {
        return new Vector2(other.X - X, other.Y - Y);
    }

    public bool Equals(Vector2 other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X + right.X, left.Y + right.Y);
    }
    
    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X - right.X, left.Y - right.Y);
    }
    
    public static Vector2 operator *(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X * right.X, left.Y * right.Y);
    }
    
    public static Vector2 operator *(Vector2 left, float right)
    {
        return new Vector2(left.X * right, left.Y * right);
    }
    
    
    public static Vector2 operator /(Vector2 left, Vector2 right)
    {
        return new Vector2(left.X / right.X, left.Y / right.Y);
    }
    
    public static Vector2 operator /(Vector2 left, float right)
    {
        return new Vector2(left.X / right, left.Y / right);
    }
    
    public static implicit operator Vector2(Vector2Int vector)
    {
        return new Vector2(vector.X, vector.Y);
    }

    public static implicit operator Vector2(Point point)
    {
        return new Vector2(point.X, point.Y);
    }
    
    public static implicit operator Point(Vector2 vector)
    {
        return new Point((int)vector.X, (int)vector.Y);
    }
    
    public override string ToString()
    {
        return $"{X}, {Y}";
    }
}
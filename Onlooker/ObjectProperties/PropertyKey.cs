namespace Onlooker.ObjectProperties;

public abstract class PropertyKey : IEquatable<PropertyKey>
{
    public abstract bool Equals(PropertyKey? other);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj)) 
            return true;
        
        if (obj.GetType() != GetType()) 
            return false;
        
        return Equals((PropertyKey)obj);
    }

    public abstract override int GetHashCode();

    public static bool operator ==(PropertyKey? left, PropertyKey? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PropertyKey? left, PropertyKey? right)
    {
        return !Equals(left, right);
    }
}
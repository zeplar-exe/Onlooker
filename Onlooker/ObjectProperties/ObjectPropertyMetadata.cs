using System.Collections.Concurrent;

namespace Onlooker.ObjectProperties;

public class ObjectPropertyMetadata
{
    private ConcurrentDictionary<PropertyKey, object> Objects { get; }

    public ObjectPropertyMetadata()
    {
        Objects = new ConcurrentDictionary<PropertyKey, object>();
    }
    
    public void AddProperty<T>(PropertyKey key, ObjectProperty<T> property)
    {
        Objects.AddOrUpdate(key, _ => property, (_, _) => property);
    }

    public bool RemoveProperty(PropertyKey key)
    {
        return Objects.TryRemove(key, out _);
    }
    
    public T? GetProperty<T>(PropertyKey key)
    {
        if (!Objects.TryGetValue(key, out var property))
            return default;

        return property is not ObjectProperty<T> tProperty ? default(T?) : tProperty;

    }
}
namespace Onlooker.Common.Extensions;

public static class CollectionExtensions
{
    public static void Overwrite<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        
        foreach (var item in items)
            collection.Add(item);
    }
}
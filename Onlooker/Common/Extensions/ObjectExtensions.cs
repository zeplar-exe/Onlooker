namespace Onlooker.Common.Extensions;

public static class ObjectExtensions
{
    public static IEnumerable<T> CreateEnumerable<T>(this T o)
    {
        return o.CreateArray().AsEnumerable();
    }

    public static T[] CreateArray<T>(this T o)
    {
        return new[] { o };
    }

    public static List<T> CreateList<T>(this T o)
    {
        return new List<T> { o };
    }
}
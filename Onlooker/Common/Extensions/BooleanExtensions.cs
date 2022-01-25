namespace Onlooker.Common;

public static class BooleanExtensions
{
    public static int AsInt(this bool b)
    {
        return b ? 1 : 0;
    }

    public static bool AsBool(this int i)
    {
        return i > 0;
    }
}
namespace Onlooker.Common.Extensions;

public static class NumericExtensions
{
    public static int Distance(this int a, int b) => Math.Max(a, b) - Math.Min(a, b);
    public static float Distance(this float a, float b) => Math.Max(a, b) - Math.Min(a, b);
    public static double Distance(this double a, double b) => Math.Max(a, b) - Math.Min(a, b);
}
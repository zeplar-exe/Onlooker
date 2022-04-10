namespace Onlooker.Common.Extensions;

public static class StringExtensions
{
    public static int SafeParseInt(this string s) => int.TryParse(s, out var n) ? n : 0;
    public static uint SafeParseUInt(this string s) => uint.TryParse(s, out var n) ? n : 0;
    public static float SafeParseFloat(this string s) => float.TryParse(s, out var n) ? n : 0;
    public static double SafeParseDouble(this string s) => double.TryParse(s, out var n) ? n : 0;
    public static decimal SafeParseDecimal(this string s) => decimal.TryParse(s, out var n) ? n : 0;
    public static bool SafeParseBool(this string s) => bool.TryParse(s, out var n) && n;

    public static string Format(this string s, params object[] objects) => string.Format(s, objects);
}
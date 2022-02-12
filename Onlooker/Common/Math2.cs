namespace Onlooker.Common;

public static class Math2
{
    public static int Lerp(int a, int b, double alpha)
    {
        return (int)Math.Round(a + alpha * (b - a));
    }
    
    public static double Lerp(double a, double b, double alpha)
    {
        return a + alpha * (b - a);
    }

    public static float Lerp(float a, float b, double alpha)
    {
        return (float)(a + alpha * (b - a));
    }

    public static int InvLerp(int a, int b, int v)
    {
        return (v - a) / (b - a);
    }
    
    public static float InvLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }
    
    public static double InvLerp(double a, double b, double v)
    {
        return (v - a) / (b - a);
    }

    public static int FloorToInt(float f)
    {
        return (int)Math.Floor(f);
    }
    
    public static int FloorToInt(double d)
    {
        return (int)Math.Floor(d);
    }
    
    public static int FloorToInt(decimal m)
    {
        return (int)Math.Floor(m);
    }
}
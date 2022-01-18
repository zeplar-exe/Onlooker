namespace Onlooker.Common;

public static class Math2
{
    public static int Lerp(int a, int b, double alpha)
    {
        return (int)Math.Round(a + (b - a) * alpha);
    }
    
    public static double Lerp(double a, double b, double alpha)
    {
        return a + (b - a) * alpha;
    }
    
    public static float Lerp(float a, float b, double alpha)
    {
        return (float)(a + (b - a) * alpha);
        // TODO: Try to get rid of this cast
    }
}
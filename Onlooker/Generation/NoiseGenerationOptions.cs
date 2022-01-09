namespace Onlooker.Generation;

public class NoiseGenerationOptions
{
    public List<NoiseFrequency> Frequencies { get; }
    public double Redistribution { get; set; }
    public double FudgeFactor { get; set; }

    public NoiseGenerationOptions()
    {
        Frequencies = new List<NoiseFrequency>();
        Redistribution = 1;
        FudgeFactor = 0;
    }
}
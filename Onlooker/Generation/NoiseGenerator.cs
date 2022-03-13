using Onlooker.Common;
using Onlooker.Common._2D;

namespace Onlooker.Generation;

public class NoiseGenerator
{
    public List<NoiseFrequency> Frequencies { get; }
    public double Redistribution { get; set; }
    public double FudgeFactor { get; set; }

    public NoiseGenerator()
    {
        Frequencies = new List<NoiseFrequency>();
        Redistribution = 1;
        FudgeFactor = 1;
    }

    public NoiseGenerator(NoiseGenerationOptions options)
    {
        Frequencies = options.Frequencies;
        Redistribution = options.Redistribution;
        FudgeFactor = options.FudgeFactor;
    }

    public Matrix2D<double> Generate(Vector2Int size, double resultMultiplier)
    {
        var array = new double[size.X, size.Y];
        var noises = new OpenSimplex2S[Frequencies.Count];

        for (var index = 0; index < Frequencies.Count; index++)
        {
            noises[index] = new OpenSimplex2S(Random.Shared.Next());
        }

        for (var x = 0; x < size.X; x++)
        {
            for (var y = 0; y < size.Y; y++)
            {
                var finalNoise = 0d;
                var amplitudeSum = 0d;

                for (var frequencyIndex = 0; frequencyIndex < Frequencies.Count; frequencyIndex++)
                {
                    var (amplitude, frequency) = Frequencies[frequencyIndex];
                    var noise = noises[frequencyIndex];

                    amplitudeSum += amplitude;
                    finalNoise += amplitude * noise.Noise2(
                        frequency * x + Random.Shared.NextDouble(), 
                        frequency * y + Random.Shared.NextDouble());
                }

                var constrainedNoise = finalNoise / amplitudeSum;

                array[x, y] = Math.Pow(constrainedNoise * FudgeFactor, Redistribution) * resultMultiplier;
            }
        } // Thanks to https://www.redblobgames.com/maps/terrain-from-noise/
        
        return new Matrix2D<double>(array);
    }

    public NoiseGenerationOptions PackOptions()
    {
        var options = new NoiseGenerationOptions { Redistribution = Redistribution, FudgeFactor = FudgeFactor };
        options.Frequencies.AddRange(Frequencies);

        return options;
    }
}
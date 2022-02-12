using Onlooker.Common.Extensions;

namespace Onlooker.IntermediateConfiguration.Game.World.Terrain;

public class TerrainTypeConfig : DescriptiveConfigFile
{
    public double Height { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }

    public TerrainTypeConfig(FileInfo source) : base(source)
    {
        
    }
}
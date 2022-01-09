namespace Onlooker.IntermediateConfiguration.World;

public class TerrainTypeConfig : DescriptiveConfigFile
{
    public double Height { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    
    public TerrainTypeConfig(FileInfo source) : base(source)
    {
        
    }
}
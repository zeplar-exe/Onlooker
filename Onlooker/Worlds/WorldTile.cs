using Onlooker.IntermediateConfiguration.World;

namespace Onlooker.Worlds;

public class WorldTile
{ 
    public TerrainTypeConfig Source { get; }
    
    public WorldTile(TerrainTypeConfig source)
    {
        Source = source;
    }
}
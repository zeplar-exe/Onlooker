using Onlooker.IntermediateConfiguration.World.Terrain;

namespace Onlooker.Worlds;

public class WorldTile
{ 
    public TerrainTypeConfig Source { get; }
    
    public WorldTile(TerrainTypeConfig source)
    {
        Source = source;
    }
}
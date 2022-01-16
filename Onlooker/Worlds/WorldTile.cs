using Onlooker.IntermediateConfiguration.Game.World.Terrain;

namespace Onlooker.Worlds;

public class WorldTile
{ 
    public TerrainTypeConfig Source { get; }
    
    public WorldTile(TerrainTypeConfig source)
    {
        Source = source;
    }
}
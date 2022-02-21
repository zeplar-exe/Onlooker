using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;
using Onlooker.ObjectProperties;
using Vector2 = Onlooker.Common.Vector2;

namespace Onlooker.Worlds;

public class WorldTile : IWorldObject
{ 
    public TerrainTypeConfig Source { get; }
    
    public Vector2Property Position { get; set; }
    public Vector2IntProperty Size { get; set; }
    public DoubleProperty Rotation { get; set; }
    
    public WorldTile(TerrainTypeConfig source)
    {
        Source = source;

        Position = new Vector2Property(new Vector2());
        Size = new Vector2IntProperty(new Vector2Int());
        Rotation = new DoubleProperty(0);
    }

    public Rectangle CreateRect() => new Rectangle(Position.Value, Size.Value);
}
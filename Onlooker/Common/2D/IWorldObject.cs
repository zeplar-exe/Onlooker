using Onlooker.ObjectProperties;

namespace Onlooker.Common._2D;

public interface IWorldObject
{
    public Vector2Property Position { get; }
    public Vector2IntProperty Size { get; }
    public DoubleProperty Rotation { get; }
}
using Onlooker.ObjectProperties;

namespace Onlooker.Monogame;

public class SceneCamera
{
    public Vector2Property Position { get; }
    public Vector2Property Viewport { get; }
    
    public SceneCamera()
    {
        Position = new Vector2Property(new Vector2());
        Viewport = new Vector2Property(new Vector2());
    }
}
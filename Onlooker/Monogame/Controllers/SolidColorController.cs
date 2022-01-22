using Microsoft.Xna.Framework;
using Onlooker.ObjectProperties;

namespace Onlooker.Monogame.Controllers;

public class SolidColorController : GameController
{
    public RectangleProperty Rectangle { get; }
    public ColorProperty Color { get; }
    private int ZIndex { get; }

    public SolidColorController(int zIndex, Color color)
    {
        Rectangle = new RectangleProperty(default);
        Color = new ColorProperty(color);
        ZIndex = zIndex;
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        var texture = GameManager.Current.Configuration.CommonConfig.Graphics.Black!;

        canvas.Draw(ZIndex, new TextureItem(texture, Rectangle, Color));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
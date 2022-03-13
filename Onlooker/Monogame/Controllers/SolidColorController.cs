using Microsoft.Xna.Framework;
using Onlooker.Common._2D;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;

namespace Onlooker.Monogame.Controllers;

public class SolidColorController : GameController
{
    public RectangleProperty Rectangle { get; }
    public ColorProperty Color { get; set; }
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
        var (x, y) = new Vector2Int(Rectangle.Value.Width, Rectangle.Value.Height);
        
        if (x == 0 || y == 0)
            return;

        canvas.Draw(ZIndex, new RectangleGraphic(Color, Rectangle));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;

namespace Onlooker.Monogame;

public class RectangleGraphic : IDrawItem
{
    private Color Color { get; }
    private Rectangle Rect { get; }
    
    public RectangleGraphic(Color color, Rectangle rect)
    {
        Color = color;
        Rect = rect;
    }
    
    public void Draw(SpriteBatch batch)
    {
        batch.Draw(TextureHelper.CreateSolidColor(Color), Rect, Color.White);
    }
}
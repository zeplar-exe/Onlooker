using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Helpers;

namespace Onlooker.Monogame.Graphics;

public class RectangleGraphic : IDrawGraphic
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
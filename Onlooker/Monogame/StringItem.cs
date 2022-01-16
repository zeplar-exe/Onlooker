using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class StringItem : IDrawItem
{
    public StringBuilder Builder { get; }
    public SpriteFont Font { get; }
    public Rectangle Rectangle { get; }
    public Color Color { get; }
    
    public StringItem(StringBuilder builder, SpriteFont font, Rectangle rectangle, Color color)
    {
        Builder = builder;
        Font = font;
        Rectangle = rectangle;
        Color = color;
    }

    public void Draw(SpriteBatch batch)
    {
        var position = new Microsoft.Xna.Framework.Vector2(Rectangle.Location.X, Rectangle.Location.Y);
        
        batch.DrawString(Font, Builder, position, Color);
    }
}
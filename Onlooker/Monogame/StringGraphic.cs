using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class StringGraphic : IDrawGraphic
{
    public StringBuilder Builder { get; }
    public SpriteFont Font { get; }
    public Rectangle Rectangle { get; }
    public Color Tint { get; }
    
    public StringGraphic(StringBuilder builder, SpriteFont font, Rectangle rectangle)
    {
        Builder = builder;
        Font = font;
        Rectangle = rectangle;
        Tint = Color.White;
    }
    
    public StringGraphic(StringBuilder builder, SpriteFont font, Rectangle rectangle, Color tint)
    {
        Builder = builder;
        Font = font;
        Rectangle = rectangle;
        Tint = tint;
    }

    public void Draw(SpriteBatch batch)
    {
        var position = new Vector2(Rectangle.Location.X, Rectangle.Location.Y);
        
        batch.DrawString(Font, Builder, position, Tint);
    }
}
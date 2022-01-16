using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class TextureItem : IDrawItem
{
    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    public Color Color { get; }
    
    public TextureItem(Texture2D texture, Rectangle rectangle, Color color)
    {
        Texture = texture;
        Rectangle = rectangle;
        Color = color;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Rectangle, Color);
    }
}
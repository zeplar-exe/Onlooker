using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class TextureItem : IDrawItem
{
    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    public Color Tint { get; }
    
    public TextureItem(Texture2D texture, Rectangle rectangle)
    {
        Texture = texture;
        Rectangle = rectangle;
        Tint = Color.White;
    }
    
    public TextureItem(Texture2D texture, Rectangle rectangle, Color tint)
    {
        Texture = texture;
        Rectangle = rectangle;
        Tint = tint;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Rectangle, Tint);
    }
}
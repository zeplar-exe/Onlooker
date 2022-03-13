using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame.Graphics;

public class TextureGraphic : IDrawGraphic
{
    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    public Color Tint { get; }
    
    public TextureGraphic(Texture2D texture, Rectangle rectangle)
    {
        Texture = texture ?? throw new ArgumentNullException(nameof(texture));
        Rectangle = rectangle;
        Tint = Color.White;
    }
    
    public TextureGraphic(Texture2D texture, Rectangle rectangle, Color tint)
    {
        Texture = texture ?? throw new ArgumentNullException(nameof(texture));
        Rectangle = rectangle;
        Tint = tint;
    }

    public void Draw(SpriteBatch batch)
    {
        batch.Draw(Texture, Rectangle, Tint);
    }
}
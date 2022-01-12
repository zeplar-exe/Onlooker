using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onlooker.Monogame;

public class DrawItem
{
    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    
    public DrawItem(Texture2D texture, Rectangle rectangle)
    {
        Texture = texture;
        Rectangle = rectangle;
    }
}
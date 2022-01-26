using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Monogame;

namespace Onlooker.Common.Helpers;

public static class TextureHelper
{
    public static Texture2D CreateSolidColor(Color color)
    {
        var texture = new Texture2D(GameManager.Current.GraphicsDevice, 1, 1);
        texture.SetData(color.CreateArray());

        return texture;
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common.Extensions;
using Onlooker.Monogame;

namespace Onlooker.Common.Helpers;

public static class TextureHelper
{
    private static Dictionary<Color, Texture2D> TextureCache { get; }

    public static Texture2D CreateSolidColor(Color color)
    {
        if (!TextureCache.TryGetValue(color, out var texture))
        {
            texture = new Texture2D(GameManager.Current.GraphicsDevice, 1, 1);
            texture.SetData(color.CreateArray());
        }

        return texture;
    }

    public static void Dispose()
    {
        foreach (var texture in TextureCache)
        {
            texture.Value.Dispose();

            TextureCache.Remove(texture.Key);
        }
    }
}
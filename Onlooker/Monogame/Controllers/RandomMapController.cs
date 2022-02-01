using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.Generation;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class RandomMapController : GameController
{
    private Texture2D? Texture { get; set; }

    public void Generate(Vector2Int size, NoiseGenerator noise)
    {
        var noiseMap = noise.Generate(size, 1).Flatten();
        
        Texture = new Texture2D(GameManager.Current.GraphicsDevice, size.X, size.Y);
        Texture.SetData(
            noiseMap
                .Select(n => Color.Lerp(Color.White, Color.Black, (float)n))
                .ToArray());
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        if (Texture == null)
            return;
        
        canvas.Draw(0, new TextureGraphic(Texture, CommonValues.ScreenRect));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
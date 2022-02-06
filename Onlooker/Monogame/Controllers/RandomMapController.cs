using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.Common.Helpers;
using Onlooker.Generation;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Monogame.Controllers;

public class RandomMapController : GameController
{
    private Vector2Int Size { get; set; }
    private Texture2D? Texture { get; set; }
    
    public int Resolution { get; set; }

    public RandomMapController()
    {
        Resolution = 4;
    }

    public void Generate(Vector2Int size, NoiseGenerator noise)
    {
        var noiseMap = noise.Generate(size, 1).Expand(Resolution);
        
        Texture = new Texture2D(GameManager.Current.GraphicsDevice, noiseMap.Width, noiseMap.Height);
        Texture.SetData(
            noiseMap
                .Select(n => Color.Lerp(Color.White, Color.Black, (float)n))
                .ToArray());
        
        Size = size; // use chunks
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        if (Texture == null)
            return;
        
        canvas.Draw(0, new TextureGraphic(Texture,
            CoordinateConverter.ToWorldCoordinates(new Rectangle(0, 0, Size.X, Size.Y))));
    }

    public override bool IsLocked()
    {
        return false;
    }
}
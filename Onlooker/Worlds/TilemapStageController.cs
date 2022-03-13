using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Onlooker.Common;
using Onlooker.Common.Helpers;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;
using Vector2 = Onlooker.Common.Vector2;

namespace Onlooker.Worlds;

public class TilemapStageController : GameController
{
    public Vector2Property CameraPosition { get; }
    public Vector2Property CameraViewportSize { get; }
    public FloatProperty CameraMoveSpeed { get; }
    
    public Matrix2D<WorldTile>? Tilemap { get; set; }

    public TilemapStageController()
    {
        CameraViewportSize = new Vector2Property(new Vector2(10, 10));
        CameraPosition = new Vector2Property(new Vector2(0, 0));
        CameraMoveSpeed = new FloatProperty(10f);
    }
    
    public override void Update(GameTime time)
    {
        var offset = new Vector2(0, 0);
        
        if (InputFrameworkController.Current.IsKeyHeld(Keys.W))
        {
            offset.Y += CameraMoveSpeed.Value;
        }
        
        if (InputFrameworkController.Current.IsKeyHeld(Keys.A))
        {
            offset.X -= CameraMoveSpeed.Value;
        }
        
        if (InputFrameworkController.Current.IsKeyHeld(Keys.S))
        {
            offset.Y -= CameraMoveSpeed.Value;
        }
        
        if (InputFrameworkController.Current.IsKeyHeld(Keys.D))
        {
            offset.X += CameraMoveSpeed.Value;
        }

        if (InputFrameworkController.Current.IsLeftMouseHeld())
        {
            offset += (Vector2)InputFrameworkController.Current.GetMouseDelta();
        }
        
        CameraPosition.Value += offset;
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        if (Tilemap == null)
            return;
        
        var cameraRect = new Rectangle(
            new Point((int)CameraPosition.Value.X, (int)CameraPosition.Value.Y), 
            CameraViewportSize.Value);
        // Console.WriteLine(cameraRect);
        // Console.WriteLine(CameraPosition); // this is stupid
        
        for (var x = 0; x < Tilemap.Width; x++)
        {
            for (var y = 0; y < Tilemap.Height; y++)
            {
                var tile = Tilemap[x, y];
                var rect = CoordinateConverter.ToScreenCoordinates(tile.CreateRect());

                if (!cameraRect.Intersects(rect))
                    continue;
                
                canvas.Draw(Layers.World, new TextureGraphic(tile.Source.IconTexture, rect));
            }
        }
    }

    public override bool IsLocked()
    {
        return false;
    }
}
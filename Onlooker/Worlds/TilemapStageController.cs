using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Onlooker.Common;
using Onlooker.Common._2D;
using Onlooker.Common.Helpers;
using Onlooker.Monogame;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;
using Vector2 = Onlooker.Common._2D.Vector2;

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
            offset -= (Vector2)InputFrameworkController.Current.GetMouseDelta();
            // Need to use -= here because math or something for intended behavior of pulling to the right moving left
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
        var widthMultiplier = CommonValues.ScreenWidth.X / CameraViewportSize.Value.X;
        var heightMultiplier = CommonValues.ScreenHeight.Y / CameraViewportSize.Value.Y;
        
        for (var x = 0; x < Tilemap.Width; x++)
        {
            for (var y = 0; y < Tilemap.Height; y++)
            {
                var tile = Tilemap[x, y];
                var rect = CoordinateConverter.ToScreenCoordinates(tile.CreateRect());
                
                if (rect.Right < cameraRect.Left)
                    break;

                if (rect.Bottom < cameraRect.Top)
                    break;
                
                if (rect.Left > cameraRect.Right)
                    goto ExitTilemapDraw; // this is a perfectly valid usage of goto
                
                if (!cameraRect.Intersects(rect))
                    continue;

                var drawRect = new Rectangle(
                     Math2.FloorToInt((rect.X - CameraPosition.Value.X) * widthMultiplier) + 1, 
                     Math2.FloorToInt((rect.Y - CameraPosition.Value.Y) * heightMultiplier) + 1, 
                     Math2.FloorToInt(rect.Width * widthMultiplier), 
                     Math2.FloorToInt(rect.Height * heightMultiplier));
                
                canvas.Draw(Layers.World, new TextureGraphic(tile.Source.IconTexture, drawRect));
            }
        }
        
        ExitTilemapDraw: ;
    }

    public override bool IsLocked()
    {
        return false;
    }
}
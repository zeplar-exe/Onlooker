using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties;
using Vector2 = Onlooker.Common.Vector2;

namespace Onlooker.Worlds;

public class TilemapStageController : GameController
{
    public Vector2Property CameraViewportSize { get; }
    public Vector2Property CameraPosition { get; }
    
    public Tilemap Tilemap { get; set; }
    public Vector2Int TileDrawSize { get; set; }

    public TilemapStageController()
    {
        CameraViewportSize = new Vector2Property(new Vector2(50, 50));
        CameraPosition = new Vector2Property(new Vector2(0, 0));

        TileDrawSize = new Vector2Int(0, 0);
    }
    
    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        var cameraRect = new Rectangle(CameraPosition.Value, CameraViewportSize.Value);

        for (var x = 0; x < Tilemap.Tiles.Width; x++)
        {
            if (!cameraRect.Contains(new Rectangle(x * TileDrawSize.X, 0, 1, 1)))
                continue;
            
            for (var y = 0; y < Tilemap.Tiles.Height; y++)
            {
                if (!cameraRect.Contains(new Rectangle(x * TileDrawSize.X, y * TileDrawSize.Y, 1, 1)))
                    continue;
                
                var tile = Tilemap.Tiles[x, y];
                var rect = new Rectangle(
                    new Point(x * TileDrawSize.X, y * TileDrawSize.Y) - (Point)CameraPosition.Value,
                    TileDrawSize);
                
                canvas.Draw(0, new TextureGraphic(tile.Source.IconTexture, rect));
            }
        }
    }

    public override bool IsLocked()
    {
        return false;
    }
}
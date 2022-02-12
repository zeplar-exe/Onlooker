using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Worlds;

public class Tilemap : GameController
{
    public Vector2Int TileDrawSize { get; }
    public Matrix2D<WorldTile> Tiles { get; }

    public Tilemap(Vector2Int size, Vector2Int tileSize)
    {
        TileDrawSize = tileSize;
        Tiles = new Matrix2D<WorldTile>(size);
    }
    
    public Tilemap(Matrix2D<WorldTile> tiles, Vector2Int tileDrawSize)
    {
        Tiles = tiles;
        TileDrawSize = tileDrawSize;
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        var rect = new Rectangle(Point.Zero, TileDrawSize);
        
        for (var x = 0; x < Tiles.Width; x++)
        {
            for (var y = 0; y < Tiles.Height; y++)
            {
                var tile = Tiles[x, y];
                
                canvas.Draw(0, new TextureGraphic(tile.Source.IconTexture, rect));

                rect = new Rectangle(rect.Location + new Point(0, TileDrawSize.Y), rect.Size);
            }
            
            rect = new Rectangle(new Point(rect.X, 0) + new Point(TileDrawSize.X, 0), rect.Size);
        }
    }

    public override bool IsLocked()
    {
        return false;
    }
}
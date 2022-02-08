using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Monogame.Controllers;
using Onlooker.Monogame.Graphics;

namespace Onlooker.Worlds;

public class Tilemap : GameController
{
    public Matrix2D<WorldTile> Tiles { get; }

    public Tilemap(Matrix2D<WorldTile> tiles)
    {
        Tiles = tiles;
    }

    public override void Update(GameTime time)
    {
        
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }
}
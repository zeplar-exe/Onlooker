using Onlooker.Common;

namespace Onlooker.Worlds;

public class Tilemap
{
    public Matrix2D<WorldTile> Tiles { get; }

    public Tilemap(Vector2Int size)
    {
        Tiles = new Matrix2D<WorldTile>(size);
    }
    
    public Tilemap(Matrix2D<WorldTile> tiles)
    {
        Tiles = tiles;
    }
}
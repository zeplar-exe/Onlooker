namespace Onlooker.Worlds;

public class Tilemap
{
    public Matrix2D<WorldTile> Tiles { get; }

    public Tilemap(Matrix2D<WorldTile> tiles)
    {
        Tiles = tiles;
    }
    
    public void Draw()
    {
        
    }
}
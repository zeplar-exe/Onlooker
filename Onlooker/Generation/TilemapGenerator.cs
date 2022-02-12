using Onlooker.Common;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;
using Onlooker.Worlds;

namespace Onlooker.Generation;

public class TilemapGenerator
{
    public Tilemap Generate(Vector2Int size,  IEnumerable<TerrainTypeConfig> pool, NoiseGenerationOptions options)
    {
        var noise = new NoiseGenerator();
        var matrix = new Matrix2D<WorldTile>(size);
        var poolArray = pool.ToArray();

        if (poolArray.Length == 0)
            return new Tilemap(matrix, new Vector2Int(0, 0));

        var heightMap = noise.Generate(size, 100);
        var tempMap = noise.Generate(size, 100);
        var humidityMap = noise.Generate(size, 100);

        for (var x = 0; x < size.X; x++)
        {
            for (var y = 0; y < size.Y; y++)
            {
                var type = poolArray.MinBy(t =>
                    Math.Abs(t.Height - heightMap[x, y]) +
                    Math.Abs(t.Temperature - tempMap[x, y]) +
                    Math.Abs(t.Humidity - humidityMap[x, y]));

                matrix[x, y] = new WorldTile(type!);
            }
        }

        return new Tilemap(matrix, new Vector2Int(0, 0));
    }
}
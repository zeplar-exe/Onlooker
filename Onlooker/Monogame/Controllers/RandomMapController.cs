using Microsoft.Xna.Framework;
using Onlooker.Common;
using Onlooker.Common.Extensions;
using Onlooker.Generation;
using Onlooker.IntermediateConfiguration;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;
using Onlooker.Monogame.Graphics;
using Onlooker.Worlds;

namespace Onlooker.Monogame.Controllers;

public class RandomMapController : GameController
{
    private Vector2Int Size { get; set; }
    public TilemapStageController? TilemapController { get; private set; }
    
    public int WorldTileSquared { get; set; }
    public int Resolution { get; set; }

    public RandomMapController()
    {
        WorldTileSquared = 20;
        Resolution = 4;
    }
    
    public void Generate(Vector2Int size, NoiseGenerator noise)
    { // TODO: Maybe use sampling to get the nearest 5 for each map
        if (TilemapController != null)
            TilemapController.Disposed = true;
        
        TilemapController = new TilemapStageController
        {
            Tilemap = new Matrix2D<WorldTile>(size),
        };

        TilemapController.CameraViewportSize.Value = new Common.Vector2(50, 50);

        var heightMap = noise.Generate(size, 100);
        var temperatureMap = noise.Generate(size, 100);
        var humidityMap = noise.Generate(size, 100);
        var terrainTypes = ConfigurationRoot.Current.GameConfig.WorldConfig.TerrainTypes.TypeConfigs;

        var index = 0d;

        for (var xIndex = 0; xIndex < size.X; xIndex++)
        {
            for (var yIndex = 0; yIndex < size.X; yIndex++)
            {
                var terrain = terrainTypes.MinBy(t => CalculateCloseness(t,
                    heightMap[xIndex, yIndex],
                    temperatureMap[xIndex, yIndex],
                    humidityMap[xIndex, yIndex]));
            
                if (terrain == null)
                    continue;

                var x = Math2.FloorToInt(index / TilemapController.Tilemap.Width);
                var y = Math2.FloorToInt(index % TilemapController.Tilemap.Height);

                var tile = new WorldTile(terrain)
                {
                    Position =
                    {
                        Value = new Common.Vector2(x * WorldTileSquared + 1, y * WorldTileSquared + 1)
                    },
                    Size = {
                        Value = new Vector2Int(WorldTileSquared, WorldTileSquared)
                    }
                };

                TilemapController.Tilemap[x, y] = tile;

                index++;
            }
        }
        
        // TODO: Move this stuff to TilemapGenerator
        TilemapController.Enabled = true;
        GameManager.Current.HookController(TilemapController);

        Size = size;
    }
    
    private static double CalculateCloseness(TerrainTypeConfig t, double height, double temperature, double humidity)
    {
        return height.Distance(t.Height) + temperature.Distance(t.Temperature) + humidity.Distance(t.Humidity);
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
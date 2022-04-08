using Microsoft.Xna.Framework;
using Onlooker.Common._2D;
using Onlooker.Common.Extensions;
using Onlooker.Generation;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Entities;
using Onlooker.Monogame.Graphics;
using Onlooker.Worlds;
using Vector2 = Onlooker.Common._2D.Vector2;

namespace Onlooker.Monogame.Controllers;

public class RandomMapController : GameController
{
    public Vector2Int Size { get; set; }
    public TilemapStageController? TilemapController { get; private set; }
    
    public int WorldTileSquared { get; set; }
    public int Resolution { get; set; }

    public RandomMapController()
    {
        WorldTileSquared = 10;
        Resolution = 4;
    }
    
    public void Generate(Vector2Int size, NoiseGenerator noise)
    {
        if (TilemapController != null)
            TilemapController.Disposed = true;
        
        TilemapController = new TilemapStageController
        {
            Tilemap = new Matrix2D<WorldTile>(size),
            CameraViewportSize =
            {
                Value = new Vector2(50, 50)
            }
        };

        var heightMap = noise.Generate(size, 100);
        var temperatureMap = noise.Generate(size, 100);
        var humidityMap = noise.Generate(size, 100);
        var terrainTypes = IModule.Get<WorldTerrainTypeModule>().TerrainTypeConfigs;

        for (var xIndex = 0; xIndex < size.X; xIndex++)
        {
            for (var yIndex = 0; yIndex < size.Y; yIndex++)
            {
                var height = heightMap[xIndex, yIndex];
                var temperature = temperatureMap[xIndex, yIndex];
                var humidity = humidityMap[xIndex, yIndex];
            
                var terrain = terrainTypes.MinBy(t => CalculateCloseness(t, height, temperature, humidity));
            
                if (terrain == null)
                    continue; // TODO: Handle this case, should terminate or use default terrain

                var tile = new WorldTile(terrain)
                {
                    Position =
                    {
                        Value = new Vector2(xIndex * WorldTileSquared + 1, yIndex * WorldTileSquared + 1)
                    },
                    Size = {
                        Value = new Vector2Int(WorldTileSquared, WorldTileSquared)
                    }
                };

                TilemapController.Tilemap[xIndex, yIndex] = tile;
            }
        }
        
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
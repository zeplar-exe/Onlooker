using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Onlooker.Common;
using Onlooker.Common.Extensions;
using Onlooker.Common.Helpers;
using Onlooker.Generation;
using Onlooker.IntermediateConfiguration;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;
using Onlooker.Monogame.Graphics;
using Onlooker.Worlds;

namespace Onlooker.Monogame.Controllers;

public class RandomMapController : GameController
{
    private Vector2Int Size { get; set; }
    public Tilemap? Tilemap { get; private set; }
    
    public int Resolution { get; set; }

    public RandomMapController()
    {
        Resolution = 4;
    }

    public void Generate(Vector2Int size, NoiseGenerator noise)
    { // Maybe use sampling to get the nearest 5 for each map
        if (Tilemap != null)
            Tilemap.Disposed = true;
        
        Tilemap = new Tilemap(new Matrix2D<WorldTile>(size), new Vector2Int(8, 8));

        var heightMap = noise.Generate(size, 100);
        var temperatureMap = noise.Generate(size, 100);
        var humidityMap = noise.Generate(size, 100);
        var terrainTypes = ConfigurationRoot.Current.GameConfig.WorldConfig.TerrainTypes.TypeConfigs;

        var index = 0d;

        using(var heightEnum = heightMap.GetEnumerator())
        using(var tempEnum = temperatureMap.GetEnumerator())
        using (var humidityEnum = humidityMap.GetEnumerator())
        {
            while (heightEnum.MoveNext() && tempEnum.MoveNext() && humidityEnum.MoveNext())
            {
                var terrain = terrainTypes.MinBy(t => CalculateCloseness(t,
                    heightEnum.Current,
                    tempEnum.Current,
                    humidityEnum.Current));
                
                if (terrain == null)
                    continue;

                var x = Math2.FloorToInt(index / Tilemap.Tiles.Width);
                var y = Math2.FloorToInt(index % Tilemap.Tiles.Height);
                
                Tilemap.Tiles[x, y] = new WorldTile(terrain);

                index++;
            }
        }

        Tilemap.Enabled = true;
        // TODO: Move this stuff to TilemapGenerator
        GameManager.Current.HookController(Tilemap);

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
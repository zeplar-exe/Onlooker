using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Game.World.Terrain;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class WorldTerrainTypeModule : IModule
{
    public List<TerrainTypeConfig> TerrainTypeConfigs { get; }

    public WorldTerrainTypeModule()
    {
        TerrainTypeConfigs = new List<TerrainTypeConfig>();
    }

    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("game/world/terrain");

        foreach (var config in ConfigFile.FromDirectory<TerrainTypeConfig>(directory))
        {
            config.UpdateAndLogFromStream(config.Source.OpenRead());

            TerrainTypeConfigs.Add(config);
        }
    }

    public void Write(ModuleRoot root)
    {
        foreach (var config in TerrainTypeConfigs)
        {
            config.WriteAndLogToStream(config.Source.OpenRead());
        }
    }
}
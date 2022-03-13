using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Game.Entities.Races;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class EntityRaceModule : IModule
{
    public List<RaceConfig> RaceConfigs { get; }

    public EntityRaceModule()
    {
        RaceConfigs = new List<RaceConfig>();
    }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("game/entities/races");

        foreach (var config in ConfigFile.FromDirectory<RaceConfig>(directory))
        {
            config.UpdateAndLogFromStream(config.Source.OpenRead());

            RaceConfigs.Add(config);
        }
    }

    public void Write(ModuleRoot root)
    {
        foreach (var config in RaceConfigs)
        {
            config.WriteAndLogToStream(config.Source.OpenRead());
        }
    }
}
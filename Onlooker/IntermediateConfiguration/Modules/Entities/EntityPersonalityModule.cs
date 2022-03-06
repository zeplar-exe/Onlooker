using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Game.Entities.Personality;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class EntityPersonalityModule : IModule
{
    public List<PersonalityConfig> PersonalityConfigs { get; }

    public EntityPersonalityModule()
    {
        PersonalityConfigs = new List<PersonalityConfig>();
    }

    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("game/entities/personalities");

        foreach (var config in ConfigFile.FromDirectory<PersonalityConfig>(directory))
        {
            config.UpdateAndLogFromStream(config.Source.OpenRead());

            PersonalityConfigs.Add(config);
        }
    }

    public void Write(ModuleRoot root)
    {
        foreach (var config in PersonalityConfigs)
        {
            config.WriteAndLogToStream(config.Source.OpenRead());
        }
    }
}
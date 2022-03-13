using Onlooker.Common.Extensions;
using Onlooker.IntermediateConfiguration.Game.Entities.Moods;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class EntityMoodModule : IModule
{
    public List<MoodConfig> MoodConfigs { get; }

    public EntityMoodModule()
    {
        MoodConfigs = new List<MoodConfig>();
    }

    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("game/entities/moods");

        foreach (var config in ConfigFile.FromDirectory<MoodConfig>(directory))
        {
            config.UpdateAndLogFromStream(config.Source.OpenRead());

            MoodConfigs.Add(config);
        }
    }

    public void Write(ModuleRoot root)
    {
        foreach (var config in MoodConfigs)
        {
            config.WriteAndLogToStream(config.Source.OpenRead());
        }
    }
}
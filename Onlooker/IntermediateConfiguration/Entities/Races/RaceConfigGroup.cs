using SettingsConfig;
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Entities.Races;

public class RaceConfigGroup : ConfigGroup
{
    public List<RaceConfig> RaceConfigs { get; }

    public RaceConfigGroup()
    {
        RaceConfigs = new List<RaceConfig>();
    }

    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        RaceConfigs.Clear();

        foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var config = new RaceConfig(file);
            config.UpdateFromStream(file.OpenRead());
            
            RaceConfigs.Add(config);
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var config in RaceConfigs)
        {
            using var stream = File.OpenWrite(Path.Join(root.FullName, config.Id + ConfigFile.Extension));
            var result = config.WriteToStream(stream);
            
            progress.Report(result);
        }
    }
}
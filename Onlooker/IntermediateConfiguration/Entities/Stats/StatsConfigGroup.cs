using SettingsConfig;
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Entities.Stats;

public class StatsConfigGroup : ConfigGroup
{
    public List<StatConfig> StatConfigs { get; }

    public StatsConfigGroup()
    {
        StatConfigs = new List<StatConfig>();
    }

    public override void UpdateFromDirectory(DirectoryInfo root, IProgress<ConfigUpdateStatus> progress)
    {
        StatConfigs.Clear();
        
        foreach (var file in root.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var document = SettingsDocument.FromStream(file.OpenRead());
            var type = document["type"]?.Value.ToString();

            switch (type)
            {
                case "alphanumeric":
                    break;
                case "numeric":
                    var config = new NumericStatConfig(file);
                    config.UpdateFromStream(file.OpenRead());
                    
                    StatConfigs.Add(config);
                    break;
                case "boolean":
                    break;
                default:
                    progress.Report(
                        new ConfigUpdateStatus($"Expected a valid 'type' in '{file.Name}'", UpdateStatusType.Invalid));
                    break;
            }
        }
    }

    public override void WriteToDirectory(DirectoryInfo root, IProgress<ConfigWriteStatus> progress)
    {
        foreach (var config in StatConfigs)
        {
            using var stream = File.OpenWrite(Path.Join(root.FullName, config.Id + ConfigFile.Extension));
            var result = config.WriteToStream(stream);
            
            progress.Report(result);
        }
    }
}
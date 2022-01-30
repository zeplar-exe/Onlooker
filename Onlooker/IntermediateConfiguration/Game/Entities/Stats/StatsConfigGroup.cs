using Onlooker.Common.StringResources.Configuration;
using Onlooker.Common.StringResources.Configuration.Stats;
using SettingsConfig;

namespace Onlooker.IntermediateConfiguration.Game.Entities.Stats;

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
                        new ConfigUpdateStatus(
                            string.Format(StatsConfigurationProgress.InvalidType, file),
                            UpdateStatusType.Invalid));
                    break;
            }
            
            progress.Report(new ConfigUpdateStatus(
                string.Format(ConfigurationProgress.FileLoaded, file), 
                UpdateStatusType.Success));
        }
    }
}
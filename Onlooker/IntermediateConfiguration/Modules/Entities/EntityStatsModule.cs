using Onlooker.Common.Extensions;
using Onlooker.Common.StringResources.Configuration.Stats;
using Onlooker.IntermediateConfiguration.Game.Entities.Stats;
using Onlooker.Monogame.Logging;
using YASF;

namespace Onlooker.IntermediateConfiguration.Modules.Entities;

public class EntityStatsModule : IModule
{
    public List<StatConfig> StatConfigs { get; }

    public EntityStatsModule()
    {
        StatConfigs = new List<StatConfig>();
    }

    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("game/entities/stats");

        foreach (var file in directory.EnumerateFiles("*.txt", SearchOption.TopDirectoryOnly))
        {
            var document = SettingsDocument.FromStream(file.OpenRead());
            var type = document["type"]?.Value.ToString();

            switch (type)
            {
                case "alphanumeric":
                    break;
                case "numeric":
                    var config = new NumericStatConfig(file);
                    
                    config.UpdateAndLogFromStream(config.Source.OpenRead());

                    StatConfigs.Add(config);
                    break;
                case "boolean":
                    break;
                default:
                    AppLoggerCommon.ConfigurationChannel.Log(
                        AppLoggerCommon.ConfigLoadingLog, 
                        LogMessageBuilder.TimestampedMessage(
                            new ConfigUpdateStatus(
                                string.Format(StatsConfigurationProgress.InvalidType, file),
                                UpdateStatusType.Invalid).ToString()));
                    break;
            }
        }
    }

    public void Write(ModuleRoot root)
    {
        foreach (var config in StatConfigs)
        {
            config.WriteAndLogToStream(config.Source.OpenRead());
        }
    }
}
using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration.Entities.Stats;

public abstract class StatConfig : DescriptiveConfigFile
{
    protected StatConfig(FileInfo source) : base(source)
    {
        
    }
}
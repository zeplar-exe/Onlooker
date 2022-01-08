using SettingsConfig.Serialization;

namespace Onlooker.IntermediateConfiguration;

public abstract class DescriptiveConfigFile : ConfigFile
{
    public string Id { get; set; }
    
    [SettingsSerializer.SerializationName("display_name")]
    [SettingsDeserializer.SerializationName("display_name")]
    public string DisplayName { get; set; }
    
    public string Description { get; set; }

    protected DescriptiveConfigFile(FileInfo source) : base(source)
    {
        
    }
}
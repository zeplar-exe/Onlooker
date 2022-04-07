using YASF.Serialization;

namespace Onlooker.IntermediateConfiguration.Settings;

public class GuiSettingsConfig : ConfigFile
{
    [SettingsSerializer.SerializationName("log_level")]
    [SettingsDeserializer.SerializationName("log_level")]
    public LogLevel LogLevel { get; set; }
    
    public GuiSettingsConfig(FileInfo source) : base(source)
    {
        
    }
}
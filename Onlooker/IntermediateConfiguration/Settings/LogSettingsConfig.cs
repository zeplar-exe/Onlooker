namespace Onlooker.IntermediateConfiguration.Settings;

public class LogSettingsConfig : ConfigFile
{
    public int Interval { get; set; }
    
    public LogSettingsConfig(FileInfo source) : base(source)
    {
        
    }
}
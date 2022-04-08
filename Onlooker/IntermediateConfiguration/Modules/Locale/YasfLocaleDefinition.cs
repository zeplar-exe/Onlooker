using YASF.Settings;

namespace Onlooker.IntermediateConfiguration.Modules.Locale;

public class YasfLocaleDefinition
{
    public FileInfo Source { get; }
    
    public Setting Setting { get; }

    public string Key => Setting.Key;

    public string StringValue => Setting.Value.ToString();
    
    public YasfLocaleDefinition(FileInfo source, Setting setting)
    {
        Source = source;
        Setting = setting;
    }
}
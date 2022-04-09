namespace Onlooker.IntermediateConfiguration.Settings;

public class LocaleSettingsConfig : ConfigFile
{
    public string Language { get; set; }

    public LocaleSettingsConfig(FileInfo source) : base(source)
    {
        
    }
}
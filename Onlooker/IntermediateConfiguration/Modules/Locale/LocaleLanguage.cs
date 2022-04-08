namespace Onlooker.IntermediateConfiguration.Modules.Locale;

public class LocaleLanguage
{
    public Dictionary<string, YasfLocaleDefinition> YasfDefinitions { get; }
    public Dictionary<string, XmlLocaleDefinitions> XmlDefinitions { get; }

    public LocaleLanguage()
    {
        YasfDefinitions = new Dictionary<string, YasfLocaleDefinition>();
        XmlDefinitions = new Dictionary<string, XmlLocaleDefinitions>();
    }

    public string? GetRawXmlAttribute(string key)
    {
        if (!XmlDefinitions.TryGetValue(key, out var xml))
            return null;

        return xml.Attributes.TryGetValue(key, out var attribute) ? attribute : null;
    }
}
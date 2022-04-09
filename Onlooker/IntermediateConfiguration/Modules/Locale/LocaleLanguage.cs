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

    public string? GetRawYasfText(string key)
    {
        return YasfDefinitions.TryGetValue(key, out var yasf) ? yasf.StringValue : null;

    }

    public string? GetRawXmlAttribute(string key, string attributeName)
    {
        if (!XmlDefinitions.TryGetValue(key, out var xml))
            return null;

        return xml.Attributes.TryGetValue(attributeName, out var attribute) ? attribute : null;
    }
}
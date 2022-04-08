using System.Xml.Linq;

namespace Onlooker.IntermediateConfiguration.Modules.Locale;

public class XmlLocaleDefinitions
{
    public FileInfo Source { get; }
    
    public XElement Element { get; }
    
    public string Key => Element.Name.ToString();
    
    public Dictionary<string, string> Attributes => Element.Attributes()
        .ToDictionary(a => a.Name.ToString(), a => a.Value);

    public Dictionary<string, XElement> Children => Element.Elements().ToDictionary(c => c.Name.ToString());
    
    public XmlLocaleDefinitions(FileInfo source, XElement element)
    {
        Source = source;
        Element = element;
    }
}
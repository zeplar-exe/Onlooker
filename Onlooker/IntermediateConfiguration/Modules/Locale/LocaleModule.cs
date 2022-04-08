using System.Xml.Linq;
using Onlooker.Common.Extensions;
using Onlooker.Monogame.Logging;
using YASF;

namespace Onlooker.IntermediateConfiguration.Modules.Locale;

public class LocaleModule : IModule
{
    public static LocaleLanguage GetLanguage(string name) =>
        ModuleRoot.Current.GetModule<LocaleModule>().Languages[name];

    public Dictionary<string, LocaleLanguage> Languages { get; }

    public LocaleModule()
    {
        Languages = new Dictionary<string, LocaleLanguage>();
    }
    
    public void Init(ModuleRoot root)
    {
        Languages.Clear();
        
        var directory = root.Directory.ToRelativeDirectory("locale");

        foreach (var languageDirectory in directory.EnumerateDirectories())
        {
            var language = new LocaleLanguage();

            foreach (var file in languageDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories))
            {
                switch (file.Extension)
                {
                    case ".yasf":
                    {
                        var document = SettingsDocument.FromStream(file.OpenRead());

                        foreach (var setting in document.Settings)
                        {
                            language.YasfDefinitions[setting.Key] = new YasfLocaleDefinition(file, setting);
                        }

                        //foreach (var error in document.Errors)
                        //{
                            //AppLoggerCommon.LocaleChannel.Log(AppLoggerCommon.LocaleErrorLog, ); 
                            // resx here and for internal errors
                            // better resx error messages with line numbers and stuff
                        //}
                        
                        break;
                    }
                    case ".xml":
                    {
                        var document = XDocument.Load(file.OpenRead());
                        // TODO: document root null and handle exceptions

                        foreach (var element in document.Root.Elements())
                        {
                            language.XmlDefinitions[element.Name.ToString()] = new XmlLocaleDefinitions(file, element);
                        }
                        
                        break;
                    }
                }
            }
            
            Languages[languageDirectory.Name] = language;
        }
    }

    public void Write(ModuleRoot root)
    {
        throw new NotImplementedException();
    }
}
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Locale;
using Onlooker.IntermediateConfiguration.Modules.Settings;

namespace Onlooker.IntermediateConfiguration.Gui.Processing.Commands;

public static class LocaleServiceCommands
{
    public static string Yasf(string key)
    {
        var languageName = IModule.Get<SettingsModule>().LocaleSettings.Language;
        var language = LocaleModule.GetLanguage(languageName);

        return language?.GetRawYasfText(key) ?? string.Empty;
    }
}
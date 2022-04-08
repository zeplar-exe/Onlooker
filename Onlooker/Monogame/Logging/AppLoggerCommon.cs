using Onlooker.Common.Helpers;

namespace Onlooker.Monogame.Logging;

public static class AppLoggerCommon
{
    public static AppLogger ErrorChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/error"));

    public static void ErrorLog(string message) => ErrorChannel.Log("error.log", message);
    public static void ErrorLog(LogMessageBuilder builder) => ErrorChannel.Log("error.log", builder);

    public static AppLogger ConfigurationChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/configuration"));
    
    public static void ConfigLoadingLog(string message) => 
        ConfigurationChannel.Log("configuration_loading.log", message);
    public static void ConfigLoadingLog(LogMessageBuilder builder) => 
        ConfigurationChannel.Log("configuration_loading.log", builder);
    
    public static AppLogger GuiLogChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/gui"));
    
    public static void GuiErrorLog(string message) => GuiLogChannel.Log("gui_error.log", message);
    public static void GuiErrorLog(LogMessageBuilder builder) => GuiLogChannel.Log("gui_error.log", builder);
    
    public static AppLogger LocaleChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/locale"));
    
    public static void LocaleErrorLog(string message) => LocaleChannel.Log("locale_error.log", message);
    public static void LocaleErrorLog(LogMessageBuilder builder) => LocaleChannel.Log("locale_error.log", builder);
}
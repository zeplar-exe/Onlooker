using Onlooker.Common.Helpers;

namespace Onlooker.Monogame.Logging;

public static class AppLoggerCommon
{
    public static AppLogger ErrorChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/error"));
    public static string ErrorLog => "error.log";

    public static AppLogger ConfigurationChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/configuration"));
    public static string ConfigLoadingLog = "configuration_loading.log";
    
    public static AppLogger GuiLogChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/gui"));
    public static string GuiErrorLog = "gui_error.log";
    
    public static AppLogger LocaleChannel = 
        AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("log/locale"));
    public static string LocaleErrorLog = "locale_error.log";
}
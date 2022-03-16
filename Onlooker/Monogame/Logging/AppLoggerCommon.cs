using Onlooker.Common.Helpers;

namespace Onlooker.Monogame.Logging;

public static class AppLoggerCommon
{
    public static AppLogger ErrorChannel = AppLogger.Channel(FileSystemHelper.FromWorkingDirectory("error"));
    public static string ErrorLog => FileSystemHelper.FromWorkingDirectory("error.log");

    public static AppLogger ConfigurationChannel = AppLogger.Channel(
        FileSystemHelper.FromWorkingDirectory("configuration"));
    public static string ConfigLoadingLog = FileSystemHelper.FromWorkingDirectory("configuration_loading.log");
}
using Onlooker.IntermediateConfiguration;
using Onlooker.Monogame.Logging;

namespace Onlooker.Common.Extensions;

public static class ConfigFileExtensions
{
    public static void UpdateAndLogFromStream(this ConfigFile configFile, Stream stream)
    {
        foreach (var update in configFile.UpdateFromStream(stream))
        {
            AppLoggerCommon.ConfigurationChannel.Log(
                AppLoggerCommon.ConfigLoadingLog,
                LogMessageBuilder.TimestampedMessage(update.ToString()));
        }
    }
    
    public static void WriteAndLogToStream(this ConfigFile configFile, Stream stream)
    {
        foreach (var update in configFile.WriteToStream(stream))
        {
            AppLoggerCommon.ConfigurationChannel.Log(
                AppLoggerCommon.ConfigLoadingLog,
                LogMessageBuilder.TimestampedMessage(update.ToString()));
        }
    }
}
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.MethodOutput.OutputTypes;
using Onlooker.IntermediateConfiguration;
using Onlooker.IntermediateConfiguration.Gui.Processing.Colors;
using Onlooker.IntermediateConfiguration.Gui.Processing.Commands;
using Onlooker.IntermediateConfiguration.Gui.Processing.Numeric;
using Onlooker.IntermediateConfiguration.Modules;
using Onlooker.IntermediateConfiguration.Modules.Settings;
using Onlooker.Monogame.Logging;

namespace Onlooker.Common.Helpers;

public static class GuiHelper
{
    public static NumericValue ParseNumericValue(XAttribute? attribute, string attributeDefault = "")
    {
        var parser = new NumericValueParser();
        var (output, value) = parser.Parse(attribute?.Value ?? attributeDefault);
        
        LogGuiOutput(output);

        return value;
    }

    public static Color ParseColor(XAttribute? attribute, string attributeDefault = "")
    {
        var parser = new ColorParser();
        var (output, value) = parser.Parse(attribute?.Value ?? attributeDefault);

        LogGuiOutput(output);

        return value;
    }

    public static CommandWrapper ParseCommand(XAttribute? attribute, string attributeDefault = "")
    {
        var parser = new CommandParser();
        var (output, value) = parser.Parse(attribute?.Value ?? attributeDefault);
        
        LogGuiOutput(output);

        return value;
    }

    public static void LogGuiOutput(params OperationOutput[] outputs)
    {
        var guiSettings = IModule.Get<SettingsModule>().GuiSettings;
        
        foreach (var output in outputs)
        {
            if (guiSettings.LogLevel is not LogLevel.None)
                return;
            
            switch (output.Type)
            {
                case OperationOutputType.Success:
                    if (guiSettings.LogLevel is not LogLevel.SuccessAndFailure or LogLevel.SuccessOnly)
                        continue;
                    break;
                case OperationOutputType.Failure:
                    if (guiSettings.LogLevel is not LogLevel.SuccessAndFailure or LogLevel.FailureOnly)
                        continue;
                    break;
            }
            
            AppLoggerCommon.GuiErrorLog(LogMessageBuilder.TimestampedMessage(output.ToString()));
        }
    }
}
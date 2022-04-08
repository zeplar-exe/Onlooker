using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.Wrappers;
using Onlooker.IntermediateConfiguration.GUI.Processing;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;

namespace Onlooker.IntermediateConfiguration.Modules.Gui;

public class GuiModule : IModule
{
    public GuiDocument MainMenu { get; set; }
    
    public void Init(ModuleRoot root)
    {
        var directory = root.Directory.ToRelativeDirectory("gui");
        var mainMenu = new FileInfo(Path.Join(directory.FullName, "main_menu.interface"));

        if (!mainMenu.Exists)
        {
            GameManager.Current.Exit();
            
            return;
        }

        var processor = new GuiProcessor();
        var result = processor.ProcessFrontendXml(XDocumentWrapper.Create(mainMenu));

        switch (result.Output.Type)
        {
            case ProcessingOutputType.Success:
                AppLoggerCommon.ConfigurationChannel.Log(AppLoggerCommon.ConfigLoadingLog, result.Output.ToString());
                break;
            case ProcessingOutputType.Corrupt:
            case ProcessingOutputType.Failure:
                AppLoggerCommon.ErrorChannel.Log(AppLoggerCommon.ErrorLog, result.Output.ToString());
                break;
        }

        MainMenu = result.Value;
    }

    public void Write(ModuleRoot root)
    {
        throw new NotImplementedException();
    }
}
using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.Wrappers;
using Onlooker.IntermediateConfiguration.Gui;
using Onlooker.IntermediateConfiguration.Gui.Processing;
using Onlooker.Monogame;
using Onlooker.Monogame.Logging;

namespace Onlooker.IntermediateConfiguration.Modules.Gui;

public class GuiModule : IModule
{
    public FrontendRoot MainMenu { get; set; }
    
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
        var (output, document) = processor.ProcessFrontendXml(XDocumentWrapper.Create(mainMenu));

        switch (output.Type)
        {
            case ProcessingOutputType.Success:
                AppLoggerCommon.ConfigLoadingLog(output.ToString());
                break;
            case ProcessingOutputType.Corrupt:
            case ProcessingOutputType.Failure:
                AppLoggerCommon.ErrorLog(output.ToString());
                break;
        }

        MainMenu = document;
    }

    public void Write(ModuleRoot root)
    {
        throw new NotImplementedException();
    }
}
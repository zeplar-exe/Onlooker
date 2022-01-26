using System.Xml.Schema;

namespace Onlooker.IntermediateConfiguration.GUI;

public class GuiConfigGroup : ConfigGroup
{
    [ConfigLocation("configuration/gui/frontend.xsd")] 
    public FrontendSchemaConfig FrontendSchema { get; set; }
}
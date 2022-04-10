using System.Xml.Linq;
using Onlooker.Common.Extensions;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Xml;
using Onlooker.Common.Wrappers;
using Onlooker.IntermediateConfiguration.Gui.Elements;

namespace Onlooker.IntermediateConfiguration.Gui.Processing;

public class GuiProcessor
{
    private const string RootElementName = "frontend_root";
    
    public OutputResult<OperationOutput, FrontendRoot> ProcessFrontendXml(XDocumentWrapper wrapper)
    {
        var root = wrapper.Document.Root;
        var result = new FrontendRoot();

        if (root == null)
            return new OutputResult<OperationOutput, FrontendRoot>(
                OperationOutput.Failure(XmlProcessingOutput.DocumentRootNull.Format(wrapper.Name)), 
                result);

        if (root.Name != RootElementName)
            return new OutputResult<OperationOutput, FrontendRoot>(
                OperationOutput.Failure(XmlProcessingOutput.InvalidDocumentRootName.Format(wrapper.Name, root.Name)),
                result);

        AddChildren(root, result.ChildElements);

        return new OutputResult<OperationOutput, FrontendRoot>(
            OperationOutput.Success(XmlProcessingOutput.DocumentProcessSuccess.Format(wrapper.Name)),
            result);
    }

    private static void AddChildren(XElement xml, ICollection<GuiElement> gui)
    {
        foreach (var element in xml.Elements())
        {
            GuiElement currentElement;
            
            switch (element.Name.ToString())
            {
                case "vertical_layout":
                    currentElement = new VerticalLayoutElement();
                    break;
                case "label":
                    currentElement = new LabelElement();
                    break;
                case "button":
                    currentElement = new ButtonElement();
                    break;
                default:
                    continue;
            }
            
            currentElement.LoadFromXml(element);
            
            AddChildren(element, currentElement.Children);
            
            gui.Add(currentElement);
        }
    }
}
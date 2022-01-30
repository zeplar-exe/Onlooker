using System.Diagnostics.Contracts;
using System.Xml.Linq;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Xml;
using Onlooker.Common.Wrappers;
using Onlooker.IntermediateConfiguration.GUI.Elements;
using Onlooker.IntermediateConfiguration.GUI.Processing.Commands;

namespace Onlooker.IntermediateConfiguration.GUI.Processing;

public class GuiProcessor
{
    public GuiProcessor()
    {
        
    }

    public OutputResult<FileProcessingOutput, GuiDocument> ProcessFrontendXml(XDocumentWrapper wrapper)
    {
        var root = wrapper.Document.Root;
        var result = new GuiDocument();

        if (root == null)
            return new OutputResult<FileProcessingOutput, GuiDocument>(
                new FileProcessingOutput(
                    ProcessingOutputType.Failure,
                    string.Format(XmlProcessingOutput.DocumentRootNull, wrapper.Name)),
                result);
        

        AddChildren(root, result.Root);

        return new OutputResult<FileProcessingOutput, GuiDocument>(
            new FileProcessingOutput(
                ProcessingOutputType.Success,
                string.Format(XmlProcessingOutput.DocumentProcessSuccess, wrapper.Name)),
            result);
    }

    private static void AddChildren(XElement xml, GuiElement gui)
    {
        foreach (var element in xml.Elements())
        {
            GuiElement? currentElement = null;
            
            switch (element.Name.ToString())
            {
                case "vertical_layout":
                    currentElement = new VerticalLayoutElement();
                    break;
            }
            
            switch (element.Name.ToString())
            {
                case "label":
                    currentElement = new LabelElement();
                    break;
                case "button":
                    currentElement = new ButtonElement();
                    break;
            }

            if (currentElement != null)
            {
                currentElement.LoadFromXml(element);
                AddChildren(element, currentElement);
                gui.Children.Add(currentElement);
            }
        }
    }
}
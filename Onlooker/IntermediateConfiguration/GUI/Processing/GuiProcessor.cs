using System.Xml.Linq;
using System.Xml.Schema;
using Onlooker.Common.MethodOutput;
using Onlooker.Common.StringResources.Xml;
using Onlooker.Common.Wrappers;
using Onlooker.Monogame;

namespace Onlooker.IntermediateConfiguration.GUI.Processing;

public class GuiProcessor
{
    public GuiProcessor()
    {
        
    }

    public OutputResult<FileProcessingOutput, GuiDocument> ProcessXml(XDocumentWrapper wrapper)
    {
        var schema = GameManager.Current.Configuration.GuiConfig.FrontendSchema;
        var errors = new List<SchemaError>();
        var set = new XmlSchemaSet(); 
        set.Add(schema.Schema!);
        
        wrapper.Document.Validate(set, (_, e) => errors.Add(new SchemaError(e)));

        if (errors.Count > 0)
        {
            return new OutputResult<FileProcessingOutput, GuiDocument>(
                new FileProcessingOutput(
                    ProcessingOutputType.Failure, 
                    string.Format(XmlProcessingOutput.InvalidXmlDocument, wrapper.Name, schema.Source.Name)),
                null);
        }

        var root = wrapper.Document.Root;

        if (root == null)
            return new OutputResult<FileProcessingOutput, GuiDocument>(
                new FileProcessingOutput(
                    ProcessingOutputType.Failure,
                    string.Format(XmlProcessingOutput.DocumentRootNull, wrapper.Name)),
                null);

        var result = new GuiDocument();

        foreach (var element in root.Elements())
        {
            switch (element.Name.ToString())
            {
                case "":
                    break;
            }
        }

        return new OutputResult<FileProcessingOutput, GuiDocument>(
            new FileProcessingOutput(
                ProcessingOutputType.Success,
                string.Format(XmlProcessingOutput.DocumentProcessSuccess, wrapper.Name)),
            result);
    }
}
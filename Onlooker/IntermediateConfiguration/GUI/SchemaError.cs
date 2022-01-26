using System.Xml.Schema;

namespace Onlooker.IntermediateConfiguration.GUI;

public readonly record struct SchemaError(XmlSchemaException Exception, XmlSeverityType Severity)
{
    public SchemaError(ValidationEventArgs e) : this(e.Exception, e.Severity)
    {
        
    }
}
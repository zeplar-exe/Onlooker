using System.Xml.Schema;
using Onlooker.IntermediateConfiguration.Game;

namespace Onlooker.IntermediateConfiguration.GUI;

public class FrontendSchemaConfig : ConfigFile
{
    public XmlSchema? Schema { get; set; }


    public FrontendSchemaConfig(FileInfo source) : base(source)
    {
        
    }

    public override ConfigUpdateStatus UpdateFromStream(Stream stream)
    {
        Schema = XmlSchema.Read(Source.OpenRead(), null);

        return new ConfigUpdateStatus("Loaded frontend.xsd (GUI XML schema)", UpdateStatusType.Success);
    }

    public override ConfigWriteStatus WriteToStream(Stream stream)
    {
        Schema?.Write(Source.OpenWrite());

        return new ConfigWriteStatus("Wrote to frontend.xsd (GUI XML schema)", WriteStatusType.Success);
    }
}
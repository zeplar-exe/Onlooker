using System.Xml.Linq;

namespace Onlooker.Common.Wrappers;

public record XDocumentWrapper(string Name, XDocument Document)
{
    public static XDocumentWrapper Create(FileInfo file)
    {
        return new XDocumentWrapper(file.Name, XDocument.Load(file.OpenRead()));
    }
}
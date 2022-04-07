using System.Text;

namespace Onlooker.IntermediateConfiguration;

public readonly record struct ConfigWriteStatus(string Message, WriteStatusType Type)
{
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(Type);
        builder.Append(": ");
        builder.Append(Message);

        return builder.ToString();
    }
}
using System.Text;

namespace Onlooker.Common.MethodOutput.OutputTypes;

public record OperationOutput(OperationOutputType Type, string Message) : IOutput
{
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(Type);
        builder.Append(' ');
        builder.Append(Message);

        return builder.ToString();
    }
}
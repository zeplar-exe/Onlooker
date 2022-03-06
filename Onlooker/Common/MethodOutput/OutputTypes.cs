using System.Text;

namespace Onlooker.Common.MethodOutput;

public record OperationOutput(OperationOutputType Type, string Message)
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

public record FileProcessingOutput(ProcessingOutputType Type, string Message)
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
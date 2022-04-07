using System.Text;

namespace Onlooker.Common.MethodOutput.OutputTypes;

public record OperationOutput(OperationOutputType Type, string Message) : IOutput
{
    public static OperationOutput Success(string message)
    {
        return new OperationOutput(OperationOutputType.Success, message);
    }
    
    public static OperationOutput Failure(string message)
    {
        return new OperationOutput(OperationOutputType.Failure, message);
    }
    
    public static OperationOutput None(string message)
    {
        return new OperationOutput(OperationOutputType.None, message);
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(Type);
        builder.Append(": ");
        builder.Append(Message);

        return builder.ToString();
    }
}
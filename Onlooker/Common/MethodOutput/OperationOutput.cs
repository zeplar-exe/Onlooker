using System.Text;
using Onlooker.Monogame.Logging;

namespace Onlooker.Common.MethodOutput;

public record OperationOutput(OperationOutputType Type, string Message) : IOperationOutput
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
    
    public bool IsSuccess()
    {
        return Type == OperationOutputType.Success;
    }

    public bool IsFailure()
    {
        return Type != OperationOutputType.Success;
    }
    
    public LogMessageBuilder CreateLog()
    {
        var builder = new LogMessageBuilder();

        builder.IncludeDate();
        builder.IncludeTime();
        
        builder.AppendText(Type.ToString());
        builder.AppendText(": ");
        builder.AppendText(Message);

        return builder;
    }
}
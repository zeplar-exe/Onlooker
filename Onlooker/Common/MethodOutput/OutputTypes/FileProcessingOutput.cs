using System.Text;

namespace Onlooker.Common.MethodOutput.OutputTypes;

public record FileProcessingOutput(ProcessingOutputType Type, string Message) : IOutput
{
    public static FileProcessingOutput Success(string message)
    {
        return new FileProcessingOutput(ProcessingOutputType.Success, message);
    }
    
    public static FileProcessingOutput Failure(string message)
    {
        return new FileProcessingOutput(ProcessingOutputType.Failure, message);
    }
    
    public static FileProcessingOutput Corrupt(string message)
    {
        return new FileProcessingOutput(ProcessingOutputType.Corrupt, message);
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append(Type);
        builder.Append(' ');
        builder.Append(Message);

        return builder.ToString();
    }
}
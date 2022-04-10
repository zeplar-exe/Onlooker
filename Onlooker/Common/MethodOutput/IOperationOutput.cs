using Onlooker.Monogame.Logging;

namespace Onlooker.Common.MethodOutput;

public interface IOperationOutput
{
    public string Message { get; }

    public bool IsSuccess();
    public bool IsFailure();

    public LogMessageBuilder CreateLog();
}
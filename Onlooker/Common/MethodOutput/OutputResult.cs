namespace Onlooker.Common.MethodOutput;

public record OutputResult<TOutMessage>(TOutMessage Output);
public record OutputResult<TOutMessage, TOut>(TOutMessage Output, TOut? Value) : OutputResult<TOutMessage>(Output);
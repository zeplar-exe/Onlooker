namespace Onlooker.Common.MethodOutput;

public record OutputResult<TOutMessage, TOut>(TOutMessage Output, TOut? Value);
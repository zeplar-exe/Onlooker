namespace Onlooker.Common.MethodOutput;

public record OperationOutput(OperationOutputType Type, string Message);
public record FileProcessingOutput(ProcessingOutputType Type, string Message);
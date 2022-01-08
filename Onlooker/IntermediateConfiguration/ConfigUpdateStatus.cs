namespace Onlooker.IntermediateConfiguration;

public readonly struct ConfigUpdateStatus
{
    public string Message { get; }
    public UpdateStatusType Type { get; }
    
    public ConfigUpdateStatus(string message, UpdateStatusType type)
    {
        Message = message;
        Type = type;
    }
}
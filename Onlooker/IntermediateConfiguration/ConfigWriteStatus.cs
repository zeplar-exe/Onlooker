namespace Onlooker.IntermediateConfiguration;

public readonly struct ConfigWriteStatus
{
    public string Message { get; }
    public WriteStatusType Type { get; }

    public ConfigWriteStatus(string message, WriteStatusType type)
    {
        Message = message;
        Type = type;
    }
}
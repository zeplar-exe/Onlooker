namespace Onlooker.ObjectProperties.Animation;

public readonly struct ProgressInfo
{
    public long CurrentStatus { get; }
    public long CompletionStatus { get; }
    
    public ProgressInfo(long currentStatus, long completionStatus)
    {
        CurrentStatus = currentStatus;
        CompletionStatus = completionStatus;
    }

    public override string ToString()
    {
        return $"{CurrentStatus} / {CompletionStatus}";
    }
}
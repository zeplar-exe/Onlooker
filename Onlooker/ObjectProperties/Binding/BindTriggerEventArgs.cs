namespace Onlooker.ObjectProperties.Binding;

public class BindTriggerEventArgs<TValue> : EventArgs
{
    public TValue? OldValue { get; }
    public TValue? NewValue { get; }
    public BindTriggerDirection Direction { get; }
    
    public BindTriggerEventArgs(TValue? oldValue, TValue? newValue, BindTriggerDirection direction)
    {
        OldValue = oldValue;
        NewValue = newValue;
        Direction = direction;
    }
}
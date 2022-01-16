namespace Onlooker.ObjectProperties.Binding;

public class ObjectPropertyValueChangedArgs<TValue> : EventArgs
{
    public TValue? OldValue { get; }
    public TValue? NewValue { get; }
    
    public ObjectPropertyValueChangedArgs(TValue? oldValue, TValue? newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}
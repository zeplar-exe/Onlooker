namespace Onlooker.ObjectProperties.Binding;

public class ObjectPropertyBinding<TValue>
{
    public ObjectProperty<TValue> Source { get; }
    public ObjectProperty<TValue> Destination { get; }

    public event EventHandler<BindTriggerEventArgs<TValue>>? BindTriggered;

    internal ObjectPropertyBinding(ObjectProperty<TValue> source, ObjectProperty<TValue> destination, BindDirection direction)
    {
        Source = source;
        Destination = destination;

        switch (direction)
        {
            case BindDirection.OneWay:
                source.ValueChanged += SourceChanged;
                break;
            case BindDirection.TwoWay:
                source.ValueChanged += SourceChanged;
                destination.ValueChanged += DestinationChanged;
                break;
        }
    }

    public void Unbind()
    {
        Source.ValueChanged -= SourceChanged;
        Destination.ValueChanged -= DestinationChanged;
    }

    private void SourceChanged(object? sender, ObjectPropertyValueChangedArgs<TValue> e)
    {
        BindTriggered?.Invoke(this, 
            new BindTriggerEventArgs<TValue>(e.OldValue, e.NewValue, BindTriggerDirection.SourceToDestination));
        Destination.Value = e.NewValue;
    }
    
    private void DestinationChanged(object? sender, ObjectPropertyValueChangedArgs<TValue> e)
    {
        BindTriggered?.Invoke(this, 
            new BindTriggerEventArgs<TValue>(e.OldValue, e.NewValue, BindTriggerDirection.DestinationToSource));
        Source.Value = e.NewValue;
    }
}
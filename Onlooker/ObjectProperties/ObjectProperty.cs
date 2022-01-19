using Onlooker.ObjectProperties.Animation;
using Onlooker.ObjectProperties.Binding;

namespace Onlooker.ObjectProperties;

public abstract class ObjectProperty<TValue>
{
    private TValue? b_value;
    public TValue? Value
    {
        get => b_value;
        set
        {
            if (b_value?.Equals(value) == true)
                return;

            var oldValue = b_value;
            
            b_value = value;
            ValueChanged?.Invoke(this, new ObjectPropertyValueChangedArgs<TValue>(oldValue, value));
        }
    }

    public event EventHandler<ObjectPropertyValueChangedArgs<TValue>>? ValueChanged;

    protected ObjectProperty(TValue value)
    {
        Value = value;
    }

    public ObjectPropertyBinding<TValue> Bind(ObjectProperty<TValue> destination, BindDirection direction)
    {
        return new ObjectPropertyBinding<TValue>(this, destination, direction);
    }

    public Animator<TValue> Animate(TValue result, AnimationSettings settings)
    {
        return new Animator<TValue>(this, result, settings);
    }
    
    protected internal abstract bool TryCreateNextFrame(TValue start, TValue end, AnimationSettings settings, out TValue next);

    public static implicit operator TValue?(ObjectProperty<TValue> property) => property.Value;
}
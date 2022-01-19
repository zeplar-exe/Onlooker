namespace Onlooker.ObjectProperties.Animation;

public class AnimationStatus<TProperty>
{
    public bool Completed { get; }
    public TProperty? NewValue { get; }

    public AnimationStatus(bool completed, TProperty? newValue)
    {
        Completed = completed;
        NewValue = newValue;
    }
}
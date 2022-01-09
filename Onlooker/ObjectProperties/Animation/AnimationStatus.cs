namespace Onlooker.ObjectProperties.Animation;

public class AnimationStatus<TProperty>
{
    public bool Completed { get; }
    public ProgressInfo Progress { get; }
    public TProperty NewValue { get; }

    public AnimationStatus(bool completed, ProgressInfo progress, TProperty newValue)
    {
        Completed = completed;
        Progress = progress;
        NewValue = newValue;
    }
}
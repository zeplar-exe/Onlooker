using Onlooker.Common;

namespace Onlooker.ObjectProperties.Animation;

public class Animator<TProperty>
{
    private CancellationToken CancellationToken { get; set; }
    
    private TProperty InitialValue { get; }
    private TProperty FinalValue { get; }
    private AnimationSettings Settings { get; }
    private ObjectProperty<TProperty> Property { get; }

    public event EventHandler? Completed;

    private IProgress<AnimationStatus<TProperty>> InterfaceProgress => Progress;
    public Progress<AnimationStatus<TProperty>> Progress { get; }

    internal Animator(ObjectProperty<TProperty> property, TProperty finalValue, AnimationSettings settings)
    {
        Property = property ?? throw new ArgumentNullException(nameof(property));
        InitialValue = Property.Value ?? throw new NullReferenceException("Cannot animate from a null initial value.");
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        FinalValue = finalValue ?? throw new ArgumentNullException(nameof(finalValue));
        
        Progress = new Progress<AnimationStatus<TProperty>>();
    }

    public IEnumerable<TProperty> GetPropertySequence()
    {
        while (Property.TryCreateNextFrame(InitialValue, FinalValue, Settings, out var frame))
        {
            Property.Value = frame;
            
            yield return frame;
        }

        Property.Value = InitialValue;
    }
    
    public void UpdateCancellationToken(CancellationToken token)
    {
        CancellationToken = token;
    }

    private TimeSpan LastStep { get; set; } = TimeSpan.FromSeconds(-50000);

    public AnimationStatus<TProperty> Step()
    {
        var status = new AnimationStatus<TProperty>(false, Property.Value);
        
        if (Time.LastUpdate.TotalGameTime - LastStep < Settings.Interval)
        {
            return status;
        }

        LastStep = Time.LastUpdate.TotalGameTime;
        
        if (CancellationToken.IsCancellationRequested)
            return status;
        
        if (Property.TryCreateNextFrame(InitialValue, FinalValue, Settings, out var frame))
        {
            Property.Value = frame;
            status = CreateStatus(frame?.Equals(FinalValue) ?? false, frame);
            InterfaceProgress.Report(status);

            if (frame?.Equals(FinalValue) ?? false)
                Completed?.Invoke(this, EventArgs.Empty);

            return status;
        }
        
        status = CreateStatus(true, frame);
        InterfaceProgress.Report(status);
        Completed?.Invoke(this, EventArgs.Empty);

        return status;
    }

    public void Reset(CancellationToken token)
    {
        Property.Value = InitialValue;
        
        UpdateCancellationToken(CancellationToken);
    }

    private AnimationStatus<TProperty> CreateStatus(bool completed, TProperty? value)
    {
        return new AnimationStatus<TProperty>(completed, value);
    }
}
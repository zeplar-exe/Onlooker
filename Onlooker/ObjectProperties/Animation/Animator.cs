namespace Onlooker.ObjectProperties.Animation;

public class Animator<TProperty>
{
    private CancellationTokenSource InternalCancellation { get; }
    private CancellationToken CancellationToken { get; set; }
    
    private TProperty InitialValue { get; }
    private TProperty FinalValue { get; }
    private AnimationSettings Settings { get; }
    private ObjectProperty<TProperty> Property { get; }
    
    public bool IsAnimating { get; private set; }

    public event EventHandler? Completed;

    private IProgress<AnimationStatus<TProperty>> InterfaceProgress => Progress;
    public Progress<AnimationStatus<TProperty>> Progress { get; }

    internal Animator(ObjectProperty<TProperty> property, TProperty finalValue, AnimationSettings settings)
    {
        Property = property ?? throw new ArgumentNullException(nameof(property));
        InitialValue = Property.Value ?? throw new NullReferenceException("Cannot animate from a null initial value.");
        Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        FinalValue = finalValue ?? throw new ArgumentNullException(nameof(finalValue));

        InternalCancellation = new CancellationTokenSource();
        Progress = new Progress<AnimationStatus<TProperty>>();
    }

    public IEnumerable<TProperty> GetPropertySequence()
    {
        if (IsAnimating)
            throw new InvalidOperationException(
                "The animator has already been started, calling GetPropertySequence could corrupt memory.");

        while (Property.TryCreateNextFrame(InitialValue, FinalValue, Settings, out var frame))
        {
            Property.Value = frame;
            
            yield return frame;
        }

        Property.Value = InitialValue;
    }
    
    public async Task Start(CancellationToken token)
    {
        if (IsAnimating)
            throw new InvalidOperationException("The animator has already been started.");
        
        IsAnimating = true;
        CancellationToken = token;
        
        await AnimateAsync();
    }

    public void Stop()
    {
        if (!IsAnimating)
            throw new InvalidOperationException("The animator has already been stopped or is not animating yet.");
        
        IsAnimating = false;
    }

    public async Task Restart(CancellationToken token)
    {
        InternalCancellation.Cancel();
        Property.Value = InitialValue;
        IsAnimating = true;
        
        await Start(CancellationToken);
    }

    private async Task AnimateAsync()
    {
        TProperty? frame;

        while (Property.TryCreateNextFrame(InitialValue, FinalValue, Settings, out frame))
        {
            if (InternalCancellation.IsCancellationRequested)
                return;
            
            if (CancellationToken.IsCancellationRequested)
                return;
            
            Property.Value = frame;
            InterfaceProgress.Report(CreateStatus(false, frame));

            await Task.Delay(Settings.Interval, CancellationToken);
        }
        
        InterfaceProgress.Report(CreateStatus(true, frame));
        
        Completed?.Invoke(this, EventArgs.Empty);
    }

    private AnimationStatus<TProperty> CreateStatus(bool completed, TProperty? value)
    {
        return new AnimationStatus<TProperty>(completed, value);
    }
}
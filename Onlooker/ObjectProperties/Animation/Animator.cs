namespace Onlooker.ObjectProperties.Animation;

public class Animator<TProperty>
{
    private CancellationToken CancellationToken { get; set; }

    private int Index { get; set; }
    private ObjectProperty<TProperty> Property { get; }
    private TProperty[] Values { get; }
    private double Interval { get; }
    
    public bool IsAnimating { get; private set; }

    public event EventHandler? Completed;

    private IProgress<AnimationStatus<TProperty>> InterfaceProgress => Progress;
    public Progress<AnimationStatus<TProperty>> Progress { get; }

    internal Animator(ObjectProperty<TProperty> property, IEnumerable<TProperty> values, double interval)
    {
        if (values == null) 
            throw new ArgumentNullException(nameof(values));
        
        Property = property ?? throw new ArgumentNullException(nameof(property));
        Values = values.ToArray();
        Interval = interval;

        Progress = new Progress<AnimationStatus<TProperty>>();
    }
    
    public IEnumerable<TProperty> GetPropertySequence() => Values;
    
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
        if (IsAnimating)
            throw new InvalidOperationException("The animator has already been started.");
        
        Index = 0;
        IsAnimating = true;
        
        await Start(CancellationToken);
    }

    private async Task AnimateAsync()
    {
        for (; Index < Values.LongLength; Index++)
        {
            if (CancellationToken.IsCancellationRequested)
                return;
            
            var value = Values[Index];
                        
            Property.Value = value;
            InterfaceProgress.Report(CreateStatus());

            await Task.Delay(TimeSpan.FromSeconds(Interval), CancellationToken);
        }
        
        Completed?.Invoke(this, EventArgs.Empty);
    }

    private AnimationStatus<TProperty> CreateStatus()
    {
        return new AnimationStatus<TProperty>(
            Index == Values.LongLength - 1,
            new ProgressInfo(Index, Values.LongLength),
            Values[Index]);
    }
}
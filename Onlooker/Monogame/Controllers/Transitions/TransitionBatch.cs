namespace Onlooker.Monogame.Controllers.Transitions;

public class TransitionBatch
{
    public List<FadeTransition> Transitions { get; }
    public bool IsPlaying { get; set; }
    
    public TransitionBatch(IEnumerable<FadeTransition> transitions)
    {
        Transitions = transitions.ToList() ?? throw new ArgumentNullException(nameof(transitions));
    }

    public bool TryStep()
    {
        return Transitions.All(t => t.TryStep());
    }
}
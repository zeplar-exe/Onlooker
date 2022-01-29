using Microsoft.Xna.Framework;
using Onlooker.Common.Args;
using Onlooker.Common.Extensions;
using Onlooker.Monogame.Controllers.Transitions;
using Onlooker.Monogame.Graphics;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker.Monogame.Controllers;

public class FadeController : GameController
{
    private Queue<TransitionBatch> TransitionBatches { get; }
    private SolidColorController DisplayRectangle { get; }

    public FadeController(int zIndex, Color color)
    {
        TransitionBatches = new Queue<TransitionBatch>();
        DisplayRectangle = new SolidColorController(zIndex, color) { Enabled = true };
    }

    public override void OnStart()
    {
        GameManager.Current.HookController(DisplayRectangle);
    }
    
    public void QueueFadeTo(Color color, AnimationSettings settings)
    {
        QueueTransition(new ColorTransition(DisplayRectangle, color, settings));
    }

    public ColorTransition QueueFadeToParallel(Color color, AnimationSettings settings)
    {
        var transition = new ColorTransition(DisplayRectangle, color, settings);
        
        QueueTransitionParallel(transition);

        return transition;
    }
    
    public FillTransition QueueFillScreen(TransitionFillDirection direction, AnimationSettings settings)
    {
        var transition = new FillTransition(DisplayRectangle, direction, settings);
        
        QueueTransition(transition);

        return transition;
    }

    public void QueueFillScreenParallel(TransitionFillDirection direction, AnimationSettings settings)
    {
        QueueTransitionParallel(new FillTransition(DisplayRectangle, direction, settings));
    }

    private void QueueTransition(FadeTransition transition)
    {
        TransitionBatches.Enqueue(new TransitionBatch(transition.CreateEnumerable()));
    }

    private void QueueTransitionParallel(FadeTransition transition)
    {
        var batch = TransitionBatches.LastOrDefault();

        if (batch == null)
        {
            batch = new TransitionBatch(new List<FadeTransition>());

            TransitionBatches.Enqueue(batch);
        }
        
        batch.Transitions.Add(transition);
    }

    public void PlayNextBatch()
    {
        if (TransitionBatches.TryPeek(out var batch))
            batch.IsPlaying = true;
    }

    public void PlayAllBatches()
    {
        foreach (var batch in TransitionBatches)
        {
            batch.IsPlaying = true;
        }
    }
    
    public override void Update(GameTime time)
    {
        if (TransitionBatches.TryPeek(out var batch))
        {
            if (!batch.IsPlaying)
                return;
            
            if (!batch.TryStep())
                TransitionBatches.Dequeue();
        }
        else
        {
            switch (CompletionHandler)
            {
                case ControllerCompletionHandler.Disable:
                    Enabled = false;
                    break;
                case ControllerCompletionHandler.Dispose:
                    Disposed = true;
                    break;
            }
        }
    }

    public override void Draw(DrawCanvas canvas, GameTime time)
    {
        
    }

    public override bool IsLocked()
    {
        return false;
    }

    public override void OnDisposing(CancellationEventArgs args)
    {
        DisplayRectangle.Disposed = true;
    }
}
namespace Onlooker.Common;

public class EventCarrier
{
    private Action<EventHandler> SubscribeAction { get; }
    private Action<EventHandler> UnsubscribeAction { get; }
    
    public EventCarrier(Action<EventHandler> subscribe, Action<EventHandler> unsubscribe)
    {
        SubscribeAction = subscribe;
        UnsubscribeAction = unsubscribe;
    }

    public void Subscribe(EventHandler handler)
    {
        SubscribeAction.Invoke(handler);
    }
    
    public void Unsubscribe(EventHandler handler)
    {
        UnsubscribeAction.Invoke(handler);
    }
}
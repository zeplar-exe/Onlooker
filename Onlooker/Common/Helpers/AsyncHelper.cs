namespace Onlooker.Common.Helpers;

public static class AsyncHelper
{
    public static async Task OnInterval(Func<Task> method, TimeSpan interval, CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            await Task.Delay(interval, cancellation);

            await method.Invoke();
        }
    }
}
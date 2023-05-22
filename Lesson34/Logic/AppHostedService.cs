namespace Lesson34.Logic;

public class AppHostedService : IHostedService
{
    private Timer _actionTimer;

    // start the service
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _actionTimer = new Timer(ServiceAction, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    // on stop
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    // action
    private void ServiceAction(object _)
    {
        Console.WriteLine("Service `AppHostedService` is running...");
    }
}
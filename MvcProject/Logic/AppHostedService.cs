namespace MvcProject.Logic;

public class AppHostedService : IHostedService
{
    private Timer _actionTimer;

    // private readonly SampleContext _context;
    private readonly IServiceScopeFactory _scopeFactory;

    // public AppHostedService(SampleContext context) => _context = context;
    public AppHostedService(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

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
        // get some data from the database
        // var persons = _context.Persons.ToList();
        var context = _scopeFactory.CreateScope().ServiceProvider.GetService<SampleContext>();
        var persons = context.Persons.ToList();
        Console.WriteLine("Service `AppHostedService` is running...");
    }
}
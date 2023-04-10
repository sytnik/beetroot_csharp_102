namespace Lesson17;

public class EventClass
{
    public event EventHandler<EventArgs> MyEvent;

    public void DoSomething()
    {
        // Do something here...

        // Raise the event
        MyEvent?.Invoke(this, EventArgs.Empty);
    }
}

public class EventSubscriber
{
    public EventSubscriber(EventClass eventClass)
    {
        eventClass.MyEvent += HandleEvent;
    }

    private void HandleEvent(object sender, EventArgs e)
    {
        Console.WriteLine("Event raised!");
    }
}

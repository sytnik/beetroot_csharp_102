using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcProject.Logic;

public class LoggingFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Perform logging before the action method is executed
        Log("Action is executing");
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // Perform logging after the action method has executed
        Log("Action executed");
    }

    private void Log(string message)
    {
        // Replace this with your desired logging mechanism
        Console.WriteLine(message);
    }
}

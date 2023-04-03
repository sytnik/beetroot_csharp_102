

namespace Lesson15Lib.Exceptions;

public class PersonException : Exception
{
    public PersonException() : base(nameof(PersonException))
    {
    }

    public PersonException(string message) : base(message)
    {
    }
}
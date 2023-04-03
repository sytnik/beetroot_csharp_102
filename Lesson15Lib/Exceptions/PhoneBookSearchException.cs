namespace Lesson15Lib.Exceptions;

public class PhoneBookSearchException : Exception
{
    public PhoneBookSearchException() : base(nameof(PersonException)) { }

    public PhoneBookSearchException(string message) : base(message) { }
}